using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ClothShop.Models
{
    public class Order //модель заказа
    {
        public int OrderID { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свою фамилию")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свое отчество")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свой телефон")]
        public string Phone { get; set; }
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный email")]
        public string Email { get; set; }
        public List<CartLine> products { get; set; }//лист элементов корзины, который мы заполняем в контроллере
        public int OrderNum { get; set; } 
    }
}