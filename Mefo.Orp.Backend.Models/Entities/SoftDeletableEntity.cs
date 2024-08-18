using Mefo.Orp.Backend.Models.Entities.Abstracts;

namespace Mefo.Orp.Backend.Models.Entities;

public class SoftDeletableEntity: ISoftDeletableEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public void MarkUpdated() => UpdatedDate = DateTime.Now;

    public bool IsDeleted { get; set; }
    public void MarkDeleted() => IsDeleted = true;
}