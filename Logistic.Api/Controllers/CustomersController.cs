using Logistic.ApplicationService.Common;
using Logistic.ApplicationService.Features.Customers.Commands;
using Logistic.ApplicationService.Features.Customers.Handlers;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.ApplicationService.Features.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.Api.Controllers;

[ApiController]
[Route("api/customers")]
public sealed class CustomersController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<CustomerListItemResponse>), StatusCodes.Status200OK)]
    public Task<PagedResponse<CustomerListItemResponse>> GetCustomers(
        [FromQuery] GetCustomerListQuery query,
        [FromServices] GetCustomerListQueryHandler handler,
        CancellationToken cancellationToken) =>
        handler.Handle(query, cancellationToken);

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<CustomerResponse> GetCustomerById(
        Guid id,
        [FromServices] GetCustomerByIdQueryHandler handler,
        CancellationToken cancellationToken) =>
        handler.Handle(new GetCustomerByIdQuery { CustomerId = id }, cancellationToken);

    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CustomerResponse>> CreateCustomer(
        [FromBody] CreateCustomerRequest request,
        [FromServices] CreateCustomerCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(new CreateCustomerCommand { Request = request }, cancellationToken);
        return CreatedAtAction(nameof(GetCustomerById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public Task<CustomerResponse> UpdateCustomer(
        Guid id,
        [FromBody] UpdateCustomerRequest request,
        [FromServices] UpdateCustomerCommandHandler handler,
        CancellationToken cancellationToken) =>
        handler.Handle(new UpdateCustomerCommand { CustomerId = id, Request = request }, cancellationToken);

    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateCustomer(
        Guid id,
        [FromServices] DeactivateCustomerCommandHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.Handle(new DeactivateCustomerCommand { CustomerId = id }, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/contacts")]
    [ProducesResponseType(typeof(CustomerContactResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerContactResponse>> AddContact(
        Guid id,
        [FromBody] AddCustomerContactRequest request,
        [FromServices] AddCustomerContactCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(
            new AddCustomerContactCommand { CustomerId = id, Request = request },
            cancellationToken);

        return CreatedAtAction(nameof(GetCustomerById), new { id }, response);
    }

    [HttpPost("{id:guid}/notes")]
    [ProducesResponseType(typeof(CustomerNoteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerNoteResponse>> AddNote(
        Guid id,
        [FromBody] AddCustomerNoteRequest request,
        [FromServices] AddCustomerNoteCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(
            new AddCustomerNoteCommand { CustomerId = id, Request = request },
            cancellationToken);

        return CreatedAtAction(nameof(GetCustomerById), new { id }, response);
    }

    [HttpPost("{id:guid}/bank-accounts")]
    [ProducesResponseType(typeof(CustomerBankAccountResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerBankAccountResponse>> AddBankAccount(
        Guid id,
        [FromBody] AddCustomerBankAccountRequest request,
        [FromServices] AddCustomerBankAccountCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(
            new AddCustomerBankAccountCommand { CustomerId = id, Request = request },
            cancellationToken);

        return CreatedAtAction(nameof(GetCustomerById), new { id }, response);
    }
}
