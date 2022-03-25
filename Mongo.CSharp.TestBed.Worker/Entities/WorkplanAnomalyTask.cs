namespace Mongo.CSharp.TestBed.Worker.Entities;

public record WorkplanAnomalyTask(int Id, string Title) : WorkplanTask(Id, Title, TaskType.Anomaly)
{
    public AnomalyDetails AnomalyDetails { get; set; } = default!;
}