using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothShop.Models
{
    //я сделал так, не уверен что это верно, меня про это не спрашивал
    public static class Identification // статик класс, хранит логин и пароль для входа, помоему можно где то в конфиге это прописать, если хочешь поищи
    {
        private static string adminName = "Admin";
        private static string adminPassword = "123";

        public static bool LogIn(Login log) //метод сверяет данные из модели Login с полями выше
        {
            if(log.LoginName == adminName && log.Password == adminPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}