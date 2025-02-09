using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mefo.Orp.Backend.Models.Dtos;
using Mefo.Orp.Backend.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mefo.Orp.Backend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly UserManager<OrpUser> _userManager;
    private readonly SignInManager<OrpUser> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginController(UserManager<OrpUser> userManager, SignInManager<OrpUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        var user = new OrpUser
        {
            UserName = userDto.Username,
            Email = userDto.Email,
            Name = userDto.Name,
            Surname = userDto.Surname,
            PhoneNumber = userDto.PhoneNumber,
            Address = userDto.Address,
            CreatedDate = DateTime.Now
        };
        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            return Ok(new { message = "User created successfully" });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("resetMyPassword")]
    [Authorize]
    public async Task<IActionResult>? ResetMyPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        if (User?.Identity?.Name == null)
            return BadRequest("User not found");

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var token = await _userManager
            .GeneratePasswordResetTokenAsync(user); //only for testing. password reset token should be provided
        var resetPassResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordDto.NewPassword);
        if (!resetPassResult.Succeeded)
        {
            return BadRequest(resetPassResult.Errors);
        }

        return Ok("Password has been reset successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var result =
            await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false,
                lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var token = GenerateJwtToken(loginDto.Username);
            return Ok(new { token });
        }

        return Unauthorized(new
            { message = result.IsLockedOut ? "User account locked out" : "Invalid username or password" });
    }

    private string GenerateJwtToken(string username)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, username),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (ClaimTypes.Name, username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = creds,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

public class ResetPasswordDto
{
    //public string Token { get; set; }
    public string NewPassword { get; set; }
}