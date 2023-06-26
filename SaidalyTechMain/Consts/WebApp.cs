using System.Collections.Generic;
namespace SaidalyTechMain.Consts
{
    public static class WebApp
    {
        public static List<string> AspRoles = new List<string> { "Doctor", "Patient", "User", "Customer" };

        public const string BaseUrl = "http://saidalytechapi-001-site1.etempurl.com/";
       // public const string BaseUrl = "https://localhost:44343/";
        public const string RegisterUser = "Authorization/RegisterUser";
        public const string EditUser = "Authorization/EditUser";
        public const string ResetMyPassword = "Authorization/ResetMyPassword";
        public const string ImagePath = "drugs/";
        public const string UploadItemImage = "Item/UploadItemImage";
    }
}
