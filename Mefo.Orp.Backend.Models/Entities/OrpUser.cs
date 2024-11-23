using Mefo.Orp.Backend.Models.Entities.Abstracts;
using Mefo.Orp.Backend.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Mefo.Orp.Backend.Models.Entities;

public class OrpUser : IdentityUser<int>, ISoftDeletableEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public void MarkDeleted() => IsDeleted = true;

    public UserAuthority Authority { get; set; }

    public void MarkUpdated() => UpdatedDate = DateTime.Now;
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
}