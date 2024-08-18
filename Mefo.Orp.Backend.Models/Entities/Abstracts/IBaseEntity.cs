namespace Mefo.Orp.Backend.Models.Entities.Abstracts;

public interface IBaseEntity
{
    int Id { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }
    void MarkUpdated();
}