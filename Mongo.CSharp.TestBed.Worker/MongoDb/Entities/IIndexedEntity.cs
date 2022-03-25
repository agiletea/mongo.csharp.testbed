namespace Mongo.CSharp.TestBed.Worker.MongoDb.Entities
{
    public interface IIndexedEntity<out TId>
    {
        TId? Id { get; }
    }
}
