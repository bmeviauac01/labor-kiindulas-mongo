using System.ComponentModel.DataAnnotations;

namespace Bme.Swlab1.Mongo.Models;

public class Order
{
    public string Id { get; set; }

    public DateTime? Date { get; set; }
    public DateTime? Deadline { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public string PaymentMethod { get; set; }
    public double? Total { get; set; }
}
