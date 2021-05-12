using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothShop.Models
{
    //модель корзины
    public class Cart
    {
        private List<CartLine> listCart = new List<CartLine>(); //лист вспомогательного класса, который снизу
        public void AddItem(Product product, int quantity) //метод добавления в корзину продукта
        {
            //
            CartLine cartItem = listCart.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if(cartItem == null) //если в корзине нет элемента то новый добавляется
            {
                listCart.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else //если есть, то просто увеличивается кол-во товара
            {
                cartItem.Quantity += quantity;
            }
        }
        public void RemoveCartItem(Product product) => listCart.RemoveAll(l => l.Product.ProductID == product.ProductID); //удаление элемента из корзины
        public void Clear() => listCart.Clear();//чистка корзины
        public IEnumerable<CartLine> Lines => listCart; //получение корзины (гет)
        public decimal TotalPrice() => listCart.Sum(l => l.Product.Price * l.Quantity); //подчет цены

        public List<CartLine> GetCartLines() => listCart; 
        public void ChangeQuantity(Product product, int quantity) //метод изменения количества товара в корзине
        {
            listCart.Where(l => l.Product.ProductID == product.ProductID).FirstOrDefault().Quantity = quantity;
        }
    }
    public class CartLine //сам элемент корзины, где будет храниться продукт и количество
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        [RegularExpression(@"\d", ErrorMessage = "Пожалуйста, введите кол-во товара")]
        public int Quantity { get; set; }
    }
}