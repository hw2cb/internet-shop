using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClothShop.Models
{
    //модель хранилища продуктов, отделил от контроллера, потому что этот класс используется по всему коду
    public class ProductRepository
    {
        private List<Product> list;

        public ProductRepository() //контроллер
        {
            list = new List<Product>(); //создаем лист продуктов
            FillRepository(); //при каждом создании экземпляра запускаем метод заполнения листа из базы
        }

        private void FillRepository()
        {
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString; //строка подключения
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products", con);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())//читаем из базы и записываем в лист
                {
                    Product p = new Product//при каждом шаге цикла снова без участия конструктора создаем продукт 
                    {
                        ProductID = (int)r["Id"],
                        Name = r["Name"].ToString(),
                        Price = (decimal)r["Price"],
                        Quantity = (int)r["Quantity"],
                        Description = r["Description"].ToString()
                    };
                    list.Add(p);//добавляем его в лист
                }
            }
        }
        public IEnumerable<Product> GetRepository() => list;//метод, который возвращает лист продуктов
    }
}