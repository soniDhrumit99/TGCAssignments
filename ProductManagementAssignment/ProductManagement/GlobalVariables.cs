using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace ProductManagement
{
    public class GlobalVariables
    {
        public static HttpClient client = new HttpClient();
        public static string imagePath = "~/Images/";
        public static string[] validExts = { ".jpg", ".jpeg", ".png" };

        static GlobalVariables()
        {
            client.BaseAddress = new Uri("https://localhost:44367/api/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}