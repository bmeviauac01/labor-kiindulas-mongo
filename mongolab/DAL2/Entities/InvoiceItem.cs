namespace Bme.Swlab1.Mongo.Dal.Entities;

public class InvoiceItem
{
    public string Name { get; set; }
    public int? Amount { get; set; }
    public double? Price { get; set; }
    public int? VatPercentage { get; set; }
}
