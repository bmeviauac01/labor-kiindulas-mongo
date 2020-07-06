using System;
using System.Collections.Generic;
using mongolab.Models;

namespace mongolab.DAL
{
    public class Repository : IRepository
    {
        public IList<Product> ListProducts()
        {
            throw new NotImplementedException();
        }

        public Product FindProduct(string id)
        {
            throw new NotImplementedException();
        }

        public void InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool SellProduct(string id, int amount)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Category> ListCategories()
        {
            throw new NotImplementedException();
        }

        public IList<Order> ListOrders(string status = null)
        {
            throw new NotImplementedException();
        }

        public Order FindOrder(string id)
        {
            throw new NotImplementedException();
        }

        public void InsertOrder(Order order, Product product, int amount)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> ListCustomers()
        {
            throw new NotImplementedException();
        }

        public OrderGroups GroupOrders(int groupsCount)
        {
            throw new NotImplementedException();
        }
    }
}
