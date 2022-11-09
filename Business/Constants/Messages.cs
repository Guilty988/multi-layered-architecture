using System;
using System.Runtime.Serialization;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi!";
        public static string ProductNameInvalid = "Ürün ismi Geçersiz!";
        public static string MaintenanceTime = "Sistem Bakımda!";
        public static string ProductsListed = "Added!";
        public static string ShipperAdded = "Shipper eklendi!";
        public static string ShipperError = "Shipper Eklenemedi !";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir!";
        public static string ProductNameAlreadyExists = "Ürün isimleri Aynı Olamaz!";
        public static string CategoryLimitedExceded = "Category Limitini Aştınız!";

        public static string AuthorizationDenied = "Yetkilendirme Reddedildi";

        public static string UserRegistered = "Kayıt Oldu!";
        public static string UserNotFound = "Kullanıcı bulunamadı!";
        internal static string PasswordError;
        internal static string SuccessfulLogin;
        internal static string UserAlreadyExists;

        public static string AccessTokenCreated;
    }
}

