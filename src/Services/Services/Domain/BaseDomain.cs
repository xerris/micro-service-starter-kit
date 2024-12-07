namespace Services.Services.Domain;

public interface IEntity
{
    Guid Id { get; set; }
}
public abstract class BaseDomain : IEntity
{
    public Guid Id { get; set; }
}