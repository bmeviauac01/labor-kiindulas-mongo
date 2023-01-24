using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bme.Swlab1.Mongo.Dal.Entities;

public class CustomerSite
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Tel { get; set; }
    public string Fax { get; set; }
}
