namespace Xerris.DotNet.Data.Domain;

public interface IDeleteable
{
    public bool IsDeleted { get; set; }
}