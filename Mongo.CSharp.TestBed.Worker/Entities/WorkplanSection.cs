namespace Mongo.CSharp.TestBed.Worker.Entities;

public record WorkplanSection(int Id, string Title)
{
    public IReadOnlyList<WorkplanTask> Tasks { get; init; } = default!;
}