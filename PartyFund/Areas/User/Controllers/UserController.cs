using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyFund.Presentation.UI.Common.ViewModels;
using PartyFund.Services.Services.Abstraction;
using PartyFund.Services.Services;
using PartyFund.Presentation.UI.Common;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;

namespace PartyFund.Areas.User.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/User/
        HttpClient client;
        IUserDetailsServices iUserDetailsServices = null;
        IUserServices iUserServices = null;
        string url = ConfigurationManager.AppSettings["ApiUrl"] + "User/";
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            iUserDetailsServices = new UserDetailsServices();
            iUserServices = new UserServices();
        }

        public ActionResult Index()
        {
            return View();
        }


        #region GetUsersByAdminID
        /// <summary>
        /// GetUsersByAdminID
        /// </summary>
        /// <param name="adminID"> admin ID</param>
        /// <returns></returns>
        public async Task<JsonResult> GetUsersByAdminID(string adminID)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url+"GetUsersByAdminID/"+adminID);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<List<UserDetailViewModel>>(responseData);

                return Json(Employees,JsonRequestBehavior.AllowGet);
            }
            return Json("Error");
        }
        #endregion
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(UserDetailViewModel model )
        {
            var password = model.Password;
            var PASSWORD_BCRYPT_COST = 8; // work factor
            var PASSWORD_SALT = Utility.GetSalt(); //generated random salt
            var salt = "$2a$" + PASSWORD_BCRYPT_COST + "$" + PASSWORD_SALT;
            var pwdToHash = password + salt;
            var hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt());

            model.Password = hashToStoreInDatabase;
            model.Salt = PASSWORD_SALT;
            iUserDetailsServices.Insert(model);
            return View();
        }

    }
}
