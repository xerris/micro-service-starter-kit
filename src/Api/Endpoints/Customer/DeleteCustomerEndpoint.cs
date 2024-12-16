using Serilog;
using Services.Services;

namespace Api.Endpoints.Customer;

public class DeleteCustomerEndpoint : EndpointWithoutRequest
{
    private readonly ICustomerService service;

    public DeleteCustomerEndpoint(ICustomerService service) => this.service = service;
    
    public override void Configure()
    {
        Delete("/customers/{id:guid}");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        Log.Debug("Delete Customer: {@id}", id);
        
        await service.DeleteAsync(id);
        await SendOkAsync(ct);
    }
}