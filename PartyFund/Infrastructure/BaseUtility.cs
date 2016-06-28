using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace PartyFund.Infrastructure
{
    public class BaseUtility
    {
        //public static String GetConString = ConfigurationManager.ConnectionStrings["BlackboxMain"].ConnectionString;

        //public static String GetResourceString = "Server=192.168.0.41;Database=hitech_new;Uid=sa;Password=hitech123*;";
        //public static String GetOrangeResourceString = "Server=192.168.0.41;Database=Orange;Uid=sa;Password=hitech123*;";

        //public static String GetDisplayDateFormat = "MM/dd/yyyy";
        //public static String GetDisplayDateTimeFormat = "MM/dd/yyyy h:mm tt";
        ////public static String GetJqueryDisplayDateFormat = "MM/DD/YYYY";
        ////public static String GetJqueryDisplayTimeFormat =""
        //public static String GetSQLConversionDateFormat = "dd/MM/yyyy";

        //public static string GetSecurityConStr()
        //{
        //    return ConfigurationManager.ConnectionStrings["BlackboxSecurity"].ConnectionString;
        //}

        //internal static string GetResourceSBString()
        //{
        //    throw new NotImplementedException();
        //}

        //public static String GetPumaConString()
        //{
        //    return ConfigurationManager.ConnectionStrings["PumaCon"].ToString().Trim();
        //}

        //public static String GetKenyaConString()
        //{
        //    return ConfigurationManager.ConnectionStrings["KenyaMain"].ConnectionString;
        //}

        /// <summary>
        ///Method to create api URL
        /// </summary>  
        public static string GetApiUrl(string controllerName, string actionName)
        {
            string baseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
            string url = baseUrl+ controllerName + "/" + actionName;
            return url;
        }
        /// <summary>
        /// method to call any api using GET
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> CallGetAPI(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(url);
            return response;
        }
        //gaurav Testing(not working properly)
        public static async Task<HttpResponseMessage> CallPostAPI<T>(string url, T obj)
        {
            HttpClient clientPost = new HttpClient();
            string baseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
            HttpResponseMessage responseMessage = await clientPost.PostAsync(url, new StringContent(
           Newtonsoft.Json.JsonConvert.SerializeObject(obj),
           Encoding.UTF8, "application/json"));

            return responseMessage;
        }

        public static async Task<HttpResponseMessage> CallListPostAPI<T>(string url, List<T> obj)
        {
            HttpClient clientPost = new HttpClient();
            string baseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
            HttpResponseMessage responseMessage = await clientPost.PostAsync(url, new StringContent(
           Newtonsoft.Json.JsonConvert.SerializeObject(obj),
           Encoding.UTF8, "application/json"));

            return responseMessage;
        }
        /// <summary>
        /// method to call any api using POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramsList"></param>
        /// <returns></returns>
        //public static async Task<HttpResponseMessage> CallPostAPI(string url, string paramsList)
        //{
        //    HttpClient client = new HttpClient();
        //    //client.BaseAddress = new Uri(url);
        //    StringContent queryString = new StringContent(paramsList);
        // //   HttpResponseMessage response = await client.PostAsJsonAsync(url, paramsList);
        //    // HttpResponseMessage response = await client.PostAsync(new Uri(url), queryString);
        //    return response;
        //}


    //    public static IEnumerable<SelectListItem> GetActionsList()
    //    {
    //        List<SelectListItem> items = new List<SelectListItem> {
    //              new SelectListItem() { Value="", Text="Actions"  },
    //new SelectListItem() { Value="emailreport", Text="Email Report"  },
    //new SelectListItem() { Value="exportexcel", Text="Export To Excel" }, 
    //new SelectListItem() { Value="print", Text="Print" }
    //       };
    //        return items;
    //    }



        //public string Session { get; set; }
    }
}