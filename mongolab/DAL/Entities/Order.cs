using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bme.Swlab1.Mongo.Dal.Entities;

public class Order
{
    [BsonId]
    public ObjectId Id { get; set; }
    public ObjectId? CustomerId { get; set; }
    public ObjectId? SiteId { get; set; }

    public DateTime? Date { get; set; }
    public DateTime? Deadline { get; set; }
    public string Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderItem[] OrderItems { get; set; }
    public Invoice Invoice { get; set; }
}
