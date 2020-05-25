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
        public static string ProductNameCanNotEmpty = "Ürün Adı Boş Olamaz.";
        public static string ProductNameLength = "Ürün Adı 40 Karakterden Fazla Olamaz.";
        public static string CategoryIdCanNotEmpty = "Kategori Boş Olamaz.";
        public static string UnitPriceNotEqualZero = "Ürün Birim Fiyatı 0 Olamaz.";
        public static string QuantityPerUnitLenght = "Paket Detayı 20 Karakterden Fazla Olamaz.";
        public static string UnitPriceCanNotEmpty = "Birim Fiyatı Boş Olamaz.";
        public static string PhonePriceIsGreaterThenX = "Telefon Birim Fiyatı 2000' den Büyük Olmalıdır.";
        public static string ProductNameNotIncludeSpecialChracter = "Ürün Adı Sadece Harf ve Sayı İçermelidir.";
    }
}
