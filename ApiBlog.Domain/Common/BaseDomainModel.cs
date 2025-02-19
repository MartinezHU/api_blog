namespace Blog.Domain.Common;

public abstract class BaseDomainModel
{
    public int Id { get; set; }

    // Fecha de creación, usando DateTime.UtcNow para evitar problemas con zonas horarias
    public DateTime CreatedDate { get; set; }

    // Quien ha creado el registro
    public string? CreatedBy { get; set; }

    // Fecha de la última modificación, opcional
    public DateTime? LastModifiedDate { get; set; }

    // Quien ha modificado el registro, opcional
    public string? LastModifiedBy { get; set; }
}
