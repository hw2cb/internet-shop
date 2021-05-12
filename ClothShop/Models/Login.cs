using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ClothShop.Models
{
    //модель логина, где будет логин и пароль, что бы легко передавать по всему коду, как с Product, Order и Cart
    public class Login
    {
        [Required(ErrorMessage = "Пожалуйста, введите логин")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        public string Password { get; set; }
    }
}