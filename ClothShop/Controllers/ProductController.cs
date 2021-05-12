using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothShop.Models;

namespace ClothShop.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository repo;

        public ProductController()//конструктор контроллера, как только идет вызов /Product/..... создается экземпляр контроллера, соответственно работает этот конструтор
        {
            repo = new ProductRepository(); //создаем экземлпяр хранилища при каждом создании контроллера, то есть при каждом запуске страницы
        }

        public ViewResult Index() => View(repo.GetRepository()); //вызов представления главной страницы где все продукты
                                                                 //в представление передаем тот самый лист продуктов
                                                                 //который берем в экземпляре класса ProductRepository где идет обращение к бд
    }
}