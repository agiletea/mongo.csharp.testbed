using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class IndexedEntityBase<T> : IIndexedEntity<T>
    {
        [BsonId]
        public virtual T? Id { get; set; }
    }
}
