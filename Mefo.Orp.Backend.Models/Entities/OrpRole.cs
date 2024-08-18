using Mefo.Orp.Backend.Models.Entities.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Mefo.Orp.Backend.Models.Entities;

public class OrpRole : IdentityRole<int>, ISoftDeletableEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public void MarkDeleted() => IsDeleted = true;

    public void MarkUpdated() => UpdatedDate = DateTime.Now;
}