using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineStore
{
    internal class Program
    {
        static Customer customer = new Customer();
        static StoreDB storeDB = new StoreDB();
        static void Main(string[] args)
        {
            CreateProductData();
            PreRegistrationPage();
        }
        static void CreateProductData()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<StoreDB>());
            Product bread = new Product { Name = "Хлеб", Price = 10 };
            Product water = new Product { Name = "Вода", Price = 15 };
            Product chocolate = new Product { Name = "Шоколад", Price = 25 };
            storeDB.Products.AddRange(new List<Product> { bread, water, chocolate });
            storeDB.SaveChanges();
        }
        static void PreRegistrationPage()
        {
            Console.WriteLine("Добро пожаловать в наш интернет-магазин продуктов питания!");
            Console.WriteLine("Для оформления заказов пройдите регистрацию");
            RegistrationPage();
        }
        static void RegistrationPage()
        {
            Console.WriteLine("Введите имя:");
            customer.Name = Console.ReadLine();

            Console.WriteLine("Введите номер телефона:");
            customer.PhoneNumber = Console.ReadLine();

            Console.WriteLine("Введите логин:");
            customer.LogPassData.Login = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            customer.LogPassData.Password = Console.ReadLine();

            Console.WriteLine("Вы успешно зарегистрированы!");

            Console.WriteLine(new string('-', 40));
            ShowProductsInfo();
        }      
        static void ShowProductsInfo()
        {
            Console.WriteLine("Список продуктов:");
            foreach (var product in storeDB.Products)
            {
                Console.WriteLine($"{product.Id}. {product.Name} \nЦена: {product.Price} грн.");
            }
            CreateOrder();
        }
        static void CreateOrder()
        {
            Console.WriteLine("Для создания заказа введите номер продукта:");
            int productID = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество:");
            int quantity = int.Parse(Console.ReadLine());

            Order order = new Order();
            var productList = storeDB.Products.ToList();

            order.Products.Add(productList.Find(p => p.Id == productID));
            storeDB.Orders.Add(order);
            storeDB.SaveChanges();

            var orderList = storeDB.Orders.ToList();

            string[] orderInfo = new string[5];

            foreach (var itemOrder in orderList)
            {
                foreach (var itemProduct in itemOrder.Products)
                {                   
                    orderInfo[0] = "Покупатель: " + customer.Name;
                    orderInfo[1] = "Номер телефона " + customer.PhoneNumber;
                    orderInfo[2] = "Наименование продукта: " + itemProduct.Name;
                    orderInfo[3] = "Количество: " + quantity + " шт.";
                    orderInfo[4] = "Общая стоимость: " + (quantity * itemProduct.Price) + " грн.";
                    File.WriteAllLines("Чек заказа.txt", orderInfo);                   
                }
            }
            Console.WriteLine(new string('-', 40));
            OrderProgress();
        }
        static void OrderProgress()
        {
            Random random = new Random();
            Console.Write("Идёт оформление заказа...");
            int left = Console.CursorLeft;
            int top = Console.CursorTop;           
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(random.Next(0, 100));
                Console.SetCursorPosition(left, top);
                Console.Write($"{i}%");
                if (i == 100)

                {
                    Console.WriteLine("\nЗаказ успешно оформлен!");
                    Console.ReadKey();
                }
            }
        }
    } 
}
