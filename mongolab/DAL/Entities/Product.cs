using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bme.Swlab1.Mongo.Dal.Entities;

[BsonIgnoreExtraElements]
public class Product
{
    [BsonId]
    public ObjectId Id { get; set; }
    public ObjectId? CategoryId { get; set; }

    public string Name { get; set; }
    public double? Price { get; set; }
    public int? Stock { get; set; }
    public Vat Vat { get; set; }
}
