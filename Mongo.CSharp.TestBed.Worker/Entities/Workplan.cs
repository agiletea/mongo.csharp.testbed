namespace Mongo.CSharp.TestBed.Worker.Entities;

public record Workplan(int Id, string Title)
{
    public IReadOnlyList<WorkplanSection> Sections { get; init; } = default!;
}