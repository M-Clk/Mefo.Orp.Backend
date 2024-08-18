using Mefo.Orp.Backend.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mefo.Orp.Backend.Data.DbContexts;

public class MefoIdentityDbContext: IdentityDbContext<OrpUser, OrpRole, int>
{
    public MefoIdentityDbContext(DbContextOptions<MefoIdentityDbContext> options) : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<OrpUser>(b =>
        {
            b.ToTable("Users");
        });

        builder.Entity<OrpRole>(b =>
        {
            b.ToTable("Roles");
        });

        builder.Entity<IdentityUserRole<int>>(b =>
        {
            b.ToTable("UserRoles");
        });

        builder.Entity<IdentityUserClaim<int>>(b =>
        {
            b.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<int>>(b =>
        {
            b.ToTable("UserLogins");
        });

        builder.Entity<IdentityRoleClaim<int>>(b =>
        {
            b.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserToken<int>>(b =>
        {
            b.ToTable("UserTokens");
        });
    }
}