using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bme.Swlab1.Mongo.Dal.Entities;

public class InvoiceIssuer
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string TaxIdentifier { get; set; }
    public string BankAccount { get; set; }
}
