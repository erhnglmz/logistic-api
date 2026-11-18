namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class AddCustomerNoteRequest
{
    public string NoteText { get; init; } = string.Empty;

    public DateOnly? ContactDate { get; init; }
}
