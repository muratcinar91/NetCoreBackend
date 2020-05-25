using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Added = "Başarıyla Eklendi.";
        public static string Updated = "Başarıyla Güncellendi.";
        public static string Deleted = "Başarıyla Silindi.";

        public static string UserNotFound = "Kullanıcı Bulunamadı.";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Sisteme Giriş Başarılı";
        public static string UserAlreadyExists = "Kullanıcı Daha Önce Eklenmiştir.";
        public static string UserRegistered = "Kullanıcı Başarıyla Kaydedildi.";
        public static string AccessTokenCreated = "Access Token Başarılı ile Oluşturuldu.";
    }
}
