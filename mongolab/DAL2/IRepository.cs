using Bme.Swlab1.Mongo.Models;

namespace Bme.Swlab1.Mongo.Dal;

public interface IRepository
{
    IList<Product> ListProducts();
    Product FindProduct(string id);
    void InsertProduct(Product product);
    bool SellProduct(string id, int amount);
    void DeleteProduct(string id);

    IList<Category> ListCategories();

    IList<Order> ListOrders(string status);
    Order FindOrder(string id);
    void InsertOrder(Order order, Product product, int amount);
    bool UpdateOrder(Order order);
    void DeleteOrder(string id);

    IList<Customer> ListCustomers();

    OrderGroups GroupOrders(int groupsCount);
}
