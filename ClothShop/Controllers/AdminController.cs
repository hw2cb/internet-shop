using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClothShop.Models;
namespace ClothShop.Controllers
{
    [Authorize] //помечаю сразу весь контроллер, он становится доступен только авторизованному пользователю
    public class AdminController : Controller
    {
        ProductRepository repo = new ProductRepository(); //снова используем класс хранилище



        public ViewResult EditProductView() => View(repo.GetRepository());  //запуск представления EditProductView (редактировать количество товара на складе)


        public ViewResult AddProductForm() => View(); //запуск представления AddProductForm (добавление продуктов)

        [HttpPost]
        public ViewResult AddProduct(Product product) //метод вызывается из представления по нажатию кнопки Добавить продукт, принимает продукт
        {
            //обычная запись нового продукта в базу
            if (ModelState.IsValid)
            {

                string str = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(str))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Products (Name, Price, Quantity, Description) " +
                    "VALUES(@name, @price, @quantity, @descr)", con);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = product.Name;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = product.Price;
                    cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = product.Quantity;
                    cmd.Parameters.Add("@descr", SqlDbType.NVarChar).Value = product.Description;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return View("AddProduct", product);
            }
            else
            {
                return View("AddProductForm");
            }
        }
        [HttpPost]
        public ActionResult ChangeProductQuant(int productId, int quantity) //метод принимает id продукта и количество товара, вызывается по нажатию кнопки Изменить количество товара в представлении EditProductView
        {
            if (ModelState.IsValid)
            {
                string str = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(str))
                {
                    //обновление уже существующего продукта
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET Quantity= @quant Where Id = @productid", con);
                    cmd.Parameters.Add("@quant", SqlDbType.Int).Value = quantity;
                    cmd.Parameters.Add("@productid", SqlDbType.Int).Value = productId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("EditProductView", "Admin"); //возврат обратно
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous] //разрешает доступ не авторизованному пользователю к следующему методу, дабы залогиниться
        public ViewResult Login() => View(); //метод вызывающий представление Login, где форма заполнения логина и пароля
        [AllowAnonymous] //разрешает доступ не авторизованному пользователю к следующему методу, дабы залогиниться
        [HttpPost]
        public ActionResult Login(Login log)//метод вызывается по кнопке "Войти" из представления Login и принимает объект Login который сформировался на основе того, что ввел пользователь
        {
            if(ModelState.IsValid)
            {
                if (Identification.LogIn(log)) //здесь у статического класса вызываем метод LogIn который возвращает true если все верно или false если не верно
                {
                    FormsAuthentication.SetAuthCookie(log.LoginName, true); //авторизовываем пользователя
                    return RedirectToAction("Index", "Admin"); //и возвращаем на главную страницу для админов
                }
                else //если не верно, то не верно
                {
                    ModelState.AddModelError("", "Не верные данные");
                }
            }
            return View(log);

        }
        [HttpPost]
        public ActionResult LogOut()//метод выхода из админки, на главной админской странице есть кнопка выйти
        {
            
            FormsAuthentication.SignOut(); //выводим из авторизации пользователя
            return RedirectToAction("Index", "Product"); //тк пользователь вышел, то возвращаем его на главную где все продукты
        }
        
        public ViewResult Index() => View(); //метод возвращает представление главной админской страницы
    }
}