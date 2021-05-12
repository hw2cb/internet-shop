using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothShop.Models;

namespace ClothShop.Controllers
{
    public class CartController : Controller
    {
        //снова получаем все продукты из базы
        private ProductRepository repo; 
        public CartController()
        {

            repo = new ProductRepository();
        }
        //

        [HttpPost]
        public RedirectToRouteResult AddToCart(int productId)//метод добавления продукта в корзину, срабатывает из представления при нажатии на кнопку "Добавить корзину"
        {
            Product product = repo.GetRepository().FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SetCart(cart);
            }
            return RedirectToAction("Index", "Product");
        }
        public RedirectToRouteResult RemoveFromCart(int productId) //метод удаления из корзины, срабатывает при нажатии на кнопку "Удалить из корзины"
        {
            //получаем то мы id, а не продукт, поэтому обращаемся к хранилищу и находим нужный продукт
            Product product = repo.GetRepository().FirstOrDefault(p => p.ProductID == productId); 
            if(product!=null) //если продукт найден то удаляем
            {
                Cart cart = GetCart(); //вызываем метод который снизу, получаем корзину из сессии
                cart.RemoveCartItem(product); //удаляем с помощью метода в модели Cart
                SetCart(cart); //сохраняем корзину в сессию
            }
            return RedirectToAction("Cart","Cart"); //возвращаем на представление корзины
        }
        public RedirectToRouteResult ChangeItems (int productId, int quantity) //метод изменения кол-ва товара в корзине, срабатывает при нажатии на кнопку "Изменить кол-во товара"
        {
            //получаем то мы id, а не продукт, поэтому обращаемся к хранилищу и находим нужный продукт
            Product product = repo.GetRepository().FirstOrDefault(p => p.ProductID == productId);
            if(product!=null && product.Quantity>=quantity) //здесь проверка на то что устанавливаемое кол-во товара не больше чем на складе(в базе данных)
            {
                Cart cart = GetCart();//вызываем метод который снизу, получаем корзину из сессии
                cart.ChangeQuantity(product, quantity);//метод в модели Cart который меняет кол-во
                SetCart(cart); //сохраняем корзину в сессию
            }
            return RedirectToAction("Cart", "Cart"); //возвращаем на представление корзины
        }
        private Cart GetCart() //метод получения корзины из сессии
        {
            Cart cart;
            if(HttpContext.Session["Cart"] != null)
            {
                cart = (Cart)HttpContext.Session["Cart"];
            }
            else
            {
                cart = new Cart();
            }
            return cart;
        }
        private void SetCart(Cart cart) //метод установки корзину в сессию
        {
            HttpContext.Session.Add("Cart", cart);
        }
        public ViewResult Cart()  => View(GetCart()); //метод вызывает представление корзины (Cart) передавая туда в модель корзину
    }
}