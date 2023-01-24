using System.ComponentModel.DataAnnotations;

namespace Bme.Swlab1.Mongo.Models;

public class Product
{
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public double? Price { get; set; }
    [Required]
    public int? Stock { get; set; }
}
