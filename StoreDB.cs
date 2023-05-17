using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineStore
{
    public class StoreDB : DbContext
    {

        public StoreDB()
            : base("name=StoreDB")
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
    public class Order
    {
        public string Customer { get; set; }
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public Order()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
        }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public LogPassData LogPassData { get; set; }
        public List<Order> Orders { get; set; }
        public Customer()
        {
            LogPassData = new LogPassData();
            Orders = new List<Order>();
        }

    }
    public class LogPassData
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}