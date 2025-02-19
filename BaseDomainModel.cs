namespace ApiBlog.Domain.Common

public abstract class BaseDomainModel
{
	public int Id { get; set; }
	public DateTime? DateCreated { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastModifiedData { get; set; }
	public string? LastModifiedBy { get; set; }
}
