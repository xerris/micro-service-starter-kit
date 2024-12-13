using FluentValidation.Results;
using Xerris.DotNet.Core.Validations;
using ValidationException = Xerris.DotNet.Core.Validations.ValidationException;

namespace Api.Extensions;

public class ValidationResponse
{
    public string Property { get; init; } = default!;
    public string Message { get; init; } = default!;
}

public static class ValidationExtensions
{
    public static List<ValidationResponse> ToResponse(this IEnumerable<ValidationFailure> errors)
        => errors.Select(error => new ValidationResponse
            { Property = error.PropertyName, Message = error.ErrorMessage }).ToList();
    
    public static string FriendlyPrint(this ValidationException ex)
        => new FriendlyFormatter(ex).Message;
}