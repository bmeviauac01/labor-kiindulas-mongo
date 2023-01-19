using MongoDB.Bson;

namespace Bme.Swlab1.Mongo.Dal.Entities;

public class OrderItem
{
    public int? Amount { get; set; }
    public double? Price { get; set; }
    public ObjectId? ProductId { get; set; }
    public string Status { get; set; }
    public InvoiceItem InvoiceItem { get; set; }
}
