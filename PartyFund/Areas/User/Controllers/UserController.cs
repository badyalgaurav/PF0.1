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
using PartyFund.DataContracts.DataModel;
using System.Web.Security;

namespace PartyFund.Areas.User.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/User/
        #region Variables
        HttpClient client;
        IUserDetailsServices iUserDetailsServices = null;
        IUserServices iUserServices = null;
        string url = ConfigurationManager.AppSettings["ApiUrl"] + "UserApi/";
        #endregion

        #region Constructor
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            iUserDetailsServices = new UserDetailsServices();
            iUserServices = new UserServices();
        }
        #endregion

        #region Dashboard(Index)
        public ActionResult Index()
        {
            ViewBag.test = "test";
            return View();
        }
        #endregion

        #region GetUsersByAdminID
        /// <summary>
        /// GetUsersByAdminID
        /// </summary>
        /// <param name="adminID"> admin ID</param>
        /// <returns></returns>
        public async Task<JsonResult> GetUsersByAdminID(string adminID)
        {
            adminID = User.Identity.Name;
            HttpResponseMessage responseMessage = await client.GetAsync(url + "GetUsersByAdminID/" + adminID);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<List<UserDetailViewModel>>(responseData);

                return Json(Employees, JsonRequestBehavior.AllowGet);
            }
            return Json("Error");
        }
        #endregion

        #region Registration
        [HttpGet]
        public async Task<ActionResult> Registration()
        {
            try
            {
                string[] words = User.Identity.Name.Split('/');
                  var id = words[1];
                //this is used to send request to web api to get record for a particular user
                HttpResponseMessage responseMessage = await client.GetAsync(url + "GetByID/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<UserDetail>(responseData);
                    //here viewbag is used to display company name in readonly mode
                    ViewBag.CompanyName = result.CompanyName;
                    return View();
                }
            }
            catch(Exception e)
            {
                return View();
            }

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(UserDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                  string[] words = User.Identity.Name.Split('/');
                  var parentId = words[1];
                    var password = model.Password;
                    var PASSWORD_BCRYPT_COST = 8; // work factor
                    var PASSWORD_SALT = Utility.GetSalt(); //generated random salt
                    var salt = "$2a$" + PASSWORD_BCRYPT_COST + "$" + PASSWORD_SALT;
                    var pwdToHash = password + salt;
                    var hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt());
                    model.ParentID = !string.IsNullOrEmpty(parentId) ? Convert.ToInt32(parentId) : 0;
                    model.Password = hashToStoreInDatabase;
                    model.Salt = PASSWORD_SALT;
                    ViewBag.companyName = model.CompanyName;
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url + "RegisterApi", model);
                    //view bag is used to send notification
                    ViewBag.Status = responseMessage.IsSuccessStatusCode == true ? "Success" : "fail";
                    if (responseMessage.IsSuccessStatusCode)
                    {
                            return View();
                    }
                    return RedirectToAction("Registration", "User", new { area = "User" });

                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("",PartyFund.Controllers.AccountController.ErrorCodeToString(e.StatusCode));
                    return RedirectToAction("Registration", "User", new { area = "User" });
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

    }
}
