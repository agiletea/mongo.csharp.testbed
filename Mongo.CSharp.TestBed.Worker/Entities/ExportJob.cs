using Mongo.CSharp.TestBed.Worker.MongoDb.Entities;

namespace Mongo.CSharp.TestBed.Worker.Entities;

public record ExportJob(Guid Id) : IndexedRecordBase<Guid>(Id)
{
    public int WorkplanId { get; init; }
        
    public Workplan? Workplan { get; set; }
}