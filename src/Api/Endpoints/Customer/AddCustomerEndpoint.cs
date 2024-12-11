using Api.Models;
using Serilog;
using Services.Services;
using Xerris.DotNet.Core.Extensions;

namespace Api.Endpoints.Customer;

public class AddCustomerEndpoint : Endpoint<AddCustomerRequest, AddCustomerResponse>
{
    private readonly ICustomerService customerService;

    public AddCustomerEndpoint(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    public override void Configure()
    {
        Post("/customers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddCustomerRequest req, CancellationToken ct)
    {
        Log.Debug("AddCustomer: {@customer}", req.ToJson());
    }
}