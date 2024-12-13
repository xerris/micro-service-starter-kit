using Api.Models;
using Api.Models.Mappers;
using Api.Models.Requests.Customer;
using Api.Models.Responses;
using Api.Models.Validators;
using Serilog;
using Services.Services;
using Xerris.DotNet.Core.Extensions;

namespace Api.Endpoints.Customer;

public class AddCustomerEndpoint : Endpoint<AddCustomerRequest, AddCustomerResponse>
{
    private readonly ICustomerService service;

    public AddCustomerEndpoint(ICustomerService service)
        => this.service = service;

    public override void Configure()
    {
        Post("/customers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddCustomerRequest req, CancellationToken ct)
    {
        Log.Debug("AddCustomer: {@customer}", req.ToJson());
        req.IsValid();
        var created = await service.Create(req.ToCustomer());
        Response = created.Created();
    }
}