namespace Bme.Swlab1.Mongo.Dal.Entities;

public class Invoice
{
    public string CustomerName { get; set; }
    public string CustomerZipCode { get; set; }
    public string CustomerCity { get; set; }
    public string CustomerStreet { get; set; }
    public int? PrintedCopies { get; set; }
    public bool? Cancelled { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? PaymentDeadline { get; set; }
    public InvoiceIssuer InvoiceIssuer { get; set; }
}
