namespace Skillz.Services.Models.Validators;

public interface IValidator<T> where T : class
{
    Task IsValid(T subject);
}