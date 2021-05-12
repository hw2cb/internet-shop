using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothShop.Models;
using System.Data.SqlClient;
using System.Data;

namespace ClothShop.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepository repo = new OrderRepository();
        [HttpPost]
        public ViewResult OrderForm(Order order) //метод вызываемый из представления OrderForm работает по Post, принимает заказ (Order)
        {
            if (ModelState.IsValid)
            {
                int orderNum = repo.GetOrdersRepository().Last().OrderNum +1; //достаем последний номер заказа
                //достаю корзину из сессии что бы заполнить продукты для заказа
                Cart cart = (Cart)HttpContext.Session["Cart"];
                if (cart == null) //если корзина пуста, а хотят заказать, то говорим что нельзя
                {
                    ModelState.AddModelError("", "Корзина пуста");
                    return View();
                }
                else
                {
                    order.products = cart.GetCartLines(); //нашему заказу который пришел по Post заполняем в поле продуктов, продукты из корзины
                    string str = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString; //строка подключения
                    using (SqlConnection con = new SqlConnection(str))
                    {
                        //запись в базу, я сделал хуево, если у клиента три разных продукта, то в таблице Orders будет три одинаковых записи, отличие только в id продукта, зато работает
                        for (int i = 0; i < order.products.Count; i++)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO Orders (Name, Surname, Patronymic, Phone, Email, ProductID, Quantity, OrderNumber) " +
                            "VALUES(@name, @surname, @patr, @phone, @email, @productid, @quantity, @ordernum)", con);
                            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = order.Name;
                            cmd.Parameters.Add("@surname", SqlDbType.NVarChar).Value = order.Surname;
                            cmd.Parameters.Add("@patr", SqlDbType.NVarChar).Value = order.Patronymic;
                            cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = order.Phone;
                            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = order.Email;
                            cmd.Parameters.Add("@productid", SqlDbType.Int).Value = order.products[i].Product.ProductID;
                            cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = order.products[i].Quantity;
                            cmd.Parameters.Add("@ordernum", SqlDbType.Int).Value = orderNum;

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    return View("Thanks", order);//выводим представление где благодарим за заказ
                }
            }
            else
            {
                return View();
            }
            
        }
        [Authorize]// Авторайз,  помечаем что следующий метод доступен только авторизованному пользователю
        public ViewResult OrderView() => View(repo.GetOrdersRepository());//метод который возвращает представление OrderView (для админа)

        [HttpGet]
        public ViewResult OrderForm() => View(); //метод который вызывает OrderForm, работает по GET, когда верхний метод только по Post
    }
}