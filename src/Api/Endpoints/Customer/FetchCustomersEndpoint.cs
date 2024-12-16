using Api.Models.Mappers;
using Api.Models.Responses.Customer;
using Services.Services;

namespace Api.Endpoints.Customer;

public class FetchCustomersEndpoint : EndpointWithoutRequest<IEnumerable<CustomerModel>>
{
    private readonly ICustomerService customerService;

    public FetchCustomersEndpoint(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    public override void Configure()
    {
        Get("/customers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var nameFilter = Query<string>("name", false);
        var page = Query<int?>("page", false);
        var pageSize = Query<int?>("size", false);
        var includeContacts = Query<bool>("includeContacts", false);

        var result = await customerService.GetCustomersAsync(nameFilter!, page ?? 1, pageSize ?? 25, includeContacts)
            .SelectAwait(each => new ValueTask<CustomerModel>(each.ToModel()))
            .ToListAsync(cancellationToken: ct);
        
        await SendAsync(result, cancellation: ct);
    }
}