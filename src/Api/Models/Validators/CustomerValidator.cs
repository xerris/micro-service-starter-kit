using Xerris.DotNet.Core.Validations;

namespace Api.Models.Validators;

public static class CustomerValidator
{
    public static void IsValid(this AddCustomerRequest request) =>
        Validate.Begin()
            .IsNotNull(request, nameof(request)).Check()
            .IsNotEmpty(request.Name, nameof(request.Name))
            .Check();

}