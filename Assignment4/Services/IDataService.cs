using System;
using System.Collections.Generic;
using Assignment4.Tests;

namespace DatabaseService.Service
{
    public interface IDataService
    {
        public List<Category> GetCategories();
        public int NextId();
        public Category GetCategory(int id);
        public Category CreateCategory(string name, string description);
        public bool DeleteCategory(int id);
        public bool UpdateCategory(int id, string name, string description);
        public Product GetProduct(int id);
        public List<Product> GetProductByCategory(int id);
        public List<Product> GetProductByName(string searchString);
        public Order GetOrder(int id);
        public List<Order> GetOrders();
        public List<OrderDetails> GetOrderDetailsByOrderId(int orderId);
        public List<OrderDetails> GetOrderDetailsByProductId(int productId);
    }
}
