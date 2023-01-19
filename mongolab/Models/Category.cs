namespace Bme.Swlab1.Mongo.Models;

public class Category
{
    public string Name { get; set; }
    public string ParentCategoryName { get; set; }
    public int NumberOfProducts { get; set; }
}
