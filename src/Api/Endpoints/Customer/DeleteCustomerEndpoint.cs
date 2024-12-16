using Api.Models.Requests.Customer;
using Serilog;
using Services.Services;

namespace Api.Endpoints.Customer;

public class DeleteCustomerEndpoint : Endpoint<DeleteCustomerRequest>
{
    private readonly ICustomerService service;

    public DeleteCustomerEndpoint(ICustomerService service) => this.service = service;
    
    public override void Configure()
    {
        Delete("/customers");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteCustomerRequest req, CancellationToken ct)
    {
        Log.Debug("Delete Customer: {@id}", req.Id);
        
        await service.DeleteAsync(req.Id);
        await SendOkAsync(ct);
    }
}