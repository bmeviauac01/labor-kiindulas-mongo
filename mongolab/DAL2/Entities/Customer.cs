using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bme.Swlab1.Mongo.Dal.Entities;

public class Customer
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Name { get; set; }
    public string BankAccount { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    [BsonElement("mainSiteID")]
    public ObjectId MainSiteId { get; set; }
    public CustomerSite[] Sites { get; set; }
}
