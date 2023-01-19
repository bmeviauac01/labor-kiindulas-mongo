using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bme.Swlab1.Mongo.Dal.Entities;

public class Category
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public ObjectId? ParentCategoryId { get; set; }
}
