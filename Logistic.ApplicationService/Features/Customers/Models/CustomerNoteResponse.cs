namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class CustomerNoteResponse
{
    public Guid Id { get; init; }

    public string NoteText { get; init; } = string.Empty;

    public DateOnly? ContactDate { get; init; }

    public DateTime CreatedDate { get; init; }
}
