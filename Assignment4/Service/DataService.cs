using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Tests;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    public class DataService
    {
        public int NextId()
        {
            using (var db = new NorthwindContext())
            {
                var nextId = db.CategoriesSet.Max(x => x.Id) + 1;
                return nextId;
            }
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>A list of Categories</returns>
        public List<Category> GetCategories()
        {
            using (var db = new NorthwindContext())
            {
                var categories = db.CategoriesSet.ToList();
                return categories;
            }
        }

        /// <summary>
        /// Gets a category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Category object</returns>
        public Category GetCategory(int id)
        {
            using (var db = new NorthwindContext())
            {
                var category = db.CategoriesSet.Find(id);
                if (category == null) return null;

                return category;
            }
        }

        /// <summary>
        /// Creates a new Category
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns>A newly created Category</returns>
        public Category CreateCategory(string name, string description)
        {
            using (var db = new NorthwindContext())
            {
                if (name == null || description == null) return null;

                var category = new Category
                {
                    Id = NextId(),
                    Name = name,
                    Description = description
                };

                db.Add(category);
                db.SaveChanges();

                return category;
            }
        }

        /// <summary>
        /// Removes a Category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A bool</returns>
        public bool DeleteCategory(int id)
        {
            using (var db = new NorthwindContext())
            {
                var category = GetCategory(id);
                if (category == null) return false;

                db.Remove(category);
                db.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Update a Category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns>A bool</returns>
        public bool UpdateCategory(int id, string name, string description)
        {
            using (var db = new NorthwindContext())
            {
                var category = db.CategoriesSet.Find(id);

                if (category == null) return false;

                category.Name = name;
                category.Description = description;

                db.Update(category);
                db.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Gets a category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Category object</returns>
        public Product GetProduct(int id)
        {
            using (var db = new NorthwindContext())
            {
                var product = db.ProductsSet
                    .Include(x => x.Category).ToList()
                    .Find(x => x.Id == id);

                if (product == null) return null;

                return product;
            }
        }

        /// <summary>
        /// Gets a Product by Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Products</returns>
        public List<Product> GetProductByCategory(int id)
        {
            using (var db = new NorthwindContext())
            {
                var products = db.ProductsSet
                    .Include(x => x.Category)
                    .Where(x => x.CategoryId == id)
                    .ToList();

                if (products == null) return null;
                return products;
            }
        }

        /// <summary>
        /// Gets a Product by name
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Products</returns>
        public List<Product> GetProductByName(string searchString)
        {
            using (var db = new NorthwindContext())
            {
                var products = db.ProductsSet
                    .Where(x => x.Name.Contains(searchString))
                    .ToList();
                if (products == null) return null;

                return products;
            }
        }

        /// <summary>
        /// Gets an order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An order</returns>
        public Order GetOrder(int id)
        {
            using (var db = new NorthwindContext())
            {
                var order = db.OrdersSet
                    .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .ToList()
                    .Find(x => x.Id == id);

                if (order == null) return null;
                return order;
            }
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns>Orders</returns>
        public List<Order> GetOrders()
        {
            using (var db = new NorthwindContext())
            {
                var orders = db.OrdersSet.ToList();
                return orders;
            }
        }

        /// <summary>
        /// Gets OrderDetails by Order ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>OrderDetails</returns>
        public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            using (var db = new NorthwindContext())
            {
                var orderDetails = db.OrderDetails
                    .Include(x => x.Product)
                    .Where(x => x.OrderId == orderId)
                    .ToList();

                if (orderDetails == null) return null;

                return orderDetails;
            }
        }

        /// <summary>
        /// Gets OrderDetails by ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>OrderDetails</returns>
        public List<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            using (var db = new NorthwindContext())
            {
                var orderDetails = db.OrderDetails
                    .Include(x => x.Order)
                    .Where(x => x.ProductId == productId)
                    .ToList();

                if (orderDetails == null) return null;

                return orderDetails;
            }
        }
    }
}
