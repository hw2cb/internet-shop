using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothShop.Models
{
    public class Product
    {
        //модель продуктов

        public int ProductID { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите имя товара")] //валидация
        public string Name { get; set; }
        [RegularExpression(@"\d+", ErrorMessage = "Пожалуйста, введите цену товара")]
        public decimal Price { get; set; }
        [RegularExpression(@"\d+", ErrorMessage = "Пожалуйста, введите кол-во товара")]
        public int Quantity {get; set;}
        [Required(ErrorMessage = "Пожалуйста, введите описание товара")]
        public string Description { get; set; }
    }
}