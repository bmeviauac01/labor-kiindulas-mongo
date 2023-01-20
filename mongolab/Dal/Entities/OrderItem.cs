using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bme.Swlab1.Mongo.Dal.Entities;

public class OrderItem
{
    public int? Amount { get; set; }
    public double? Price { get; set; }
    [BsonElement("productID")]
    public ObjectId? ProductId { get; set; }
    public string Status { get; set; }
    public InvoiceItem InvoiceItem { get; set; }
}
