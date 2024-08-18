using Mefo.Orp.Backend.Models.Entities.Abstracts;

namespace Mefo.Orp.Backend.Models.Entities;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }

    public void MarkUpdated() => UpdatedDate = DateTime.Now;
}