using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClothShop.Models
{
    //модель где можно получить заказы из базы
    public class OrderRepository
    {
        private List<Order> ordersRepository; //лист заказов
        private ProductRepository repo; 
        public OrderRepository() //конструктор модели, при создании экземпляра он отрабатывает
        {
            ordersRepository = new List<Order>(); //создаем лист
            repo = new ProductRepository(); //создаем снова экземпляр хранилища где обращение в базу
            FillOrderRepository(); //и запускаем метод который заполняет поле ordersRepository заказами из базы
        }
        public IEnumerable<Order> GetOrdersRepository() => ordersRepository; //возвращаем лист заказов, IEnumerable это интерфейс в c#, его реализует List, так что можно возвращать обьекты List
        public void FillOrderRepository() //метод который заполняет лист заказами из базы
        {
            int oldOrderNum =0; //старый номер заказа
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString; //строка подключения
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Orders", con);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                //чтеление из базы и запись в объект Order
                while (r.Read())
                {
                    int neworderNum = (int)r["OrderNumber"]; //берем из бд новый номер заказа
                    if(oldOrderNum != neworderNum || oldOrderNum == 0)
                    {
                        Order p = new Order(); //при каждом шаге цикла создается новый объект
                        p.OrderID = (int)r["Id"];//заполняется
                        p.Name = r["Name"].ToString();
                        p.Surname = r["Surname"].ToString();
                        p.Patronymic = r["Patronymic"].ToString();
                        p.Phone = r["Phone"].ToString();
                        p.Email = r["Email"].ToString();
                        CartLine cartItem = new CartLine()//создаем элемент корзины, без вызова конструктора 
                        {
                            Quantity = (int)r["Quantity"], // количество
                            Product = repo.GetRepository().FirstOrDefault(c => c.ProductID == (int)r["ProductID"])//берем id продукта из базы данных, из хранилища находим продукт по id и добавляем его
                        };
                        p.OrderNum = neworderNum; 
                        p.products = new List<CartLine>() { cartItem }; //
                        ordersRepository.Add(p);
                        oldOrderNum = p.OrderNum;
                    }
                    else if(oldOrderNum == neworderNum)//если старый номер равен новому, то так я определяю что это тот же самый заказ и поэтому тупо добавляю в этот заказ продукт, тупо но работает
                    {
                        CartLine cartItem = new CartLine()
                        {
                            Quantity = (int)r["Quantity"],
                            Product = repo.GetRepository().FirstOrDefault(c => c.ProductID == (int)r["ProductID"])
                        };
                        ordersRepository.Last().products.Add(cartItem);//к последнему добавляем продуктов
                    }
                }
            }
        }
    }
}