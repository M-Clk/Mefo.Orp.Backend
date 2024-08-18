namespace Mefo.Orp.Backend.Models.Entities.Abstracts;

public interface ISoftDeletableEntity : IBaseEntity
{
    bool IsDeleted { get; set; }
    void MarkDeleted();
}