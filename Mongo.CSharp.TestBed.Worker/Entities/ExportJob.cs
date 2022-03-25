using Mongo.CSharp.TestBed.Worker.MongoDb.Entities;

namespace Mongo.CSharp.TestBed.Worker.Entities;

public record ExportJob(Guid Id) : IndexedRecordBase<Guid>(Id)
{
    public int WorkplanId { get; init; }
        
    public Workplan? Workplan { get; set; }
}

public record Workplan(int Id, string Title)
{
    public IReadOnlyList<WorkplanSection> Sections { get; init; } = default!;
}

public record WorkplanSection(int Id, string Title)
{
    public IReadOnlyList<WorkplanTask> Tasks { get; init; } = default!;
}

public record WorkplanTask(int Id, string Title, TaskType Type);

public record WorkplanAnomalyTask(int Id, string Title) : WorkplanTask(Id, Title, TaskType.Anomaly)
{
    public AnomalyDetails AnomalyDetails { get; set; } = default!;
}

public record AnomalyDetails(string AnomalyNumber, string Description);

public enum TaskType { Standard, Clerical, Anomaly }