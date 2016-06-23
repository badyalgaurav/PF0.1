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
using PartyFund.Controllers;
using PartyFund.Infrastructure;
using PartyFund.Presentation.UI.Common.Helpers;

namespace PartyFund.Areas.User.Controllers
{
    public class UserController : BaseController
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
                //split the user Cookie to get userdetailsID
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

        #region CreditDebitAmount
        //view name change to _CreditDebitUserAmount
        public async Task<ActionResult> CreditDebitUserAmount(string id, string param)
        {
        
            var userID = "1014";
     //       TransectionDetailViewModel transectionDetails = new TransectionDetailViewModel();
          var  transectionDetails = await GetTransectionDetailsByUserID(userID);
            var test = User.UserDetailsID;
            if (param == "add")
            {
                ViewBag.sign = "+";
                ViewBag.action = "Credit Amount";
            }
            else
            {
                ViewBag.sign = "-";
                ViewBag.action = "Deducted Amount";

            }
            
            return View("~/Areas/User/Views/User/_CreditDebitUserAmount.cshtml",transectionDetails);
        }
        #endregion

        public async Task<TransectionDetailViewModel> GetTransectionDetailsByUserID(string userID)
        {
            TransectionDetailViewModel transectionDetails = new TransectionDetailViewModel();
            #region to call GetTransectionDetailsByUserID
            //Create URL
            string url = BaseUtility.GetApiUrl("TransectionDetailsAPI", "GetTransectionDetailsByUserID");
            url = string.Format(url + "?userID={0}", userID);

            //Invoke API
            HttpResponseMessage messageTypeList = await BaseUtility.CallGetAPI(url);

            //Check API response
            if (messageTypeList.IsSuccessStatusCode)
            {
                var jsonString = await messageTypeList.Content.ReadAsStringAsync();
                transectionDetails = JsonConvert.DeserializeObject<TransectionDetailViewModel>(jsonString);
            }
            #endregion
            return transectionDetails;
        }
        public async Task<JsonResult> GetUsersByAdminIdDT(JqueryDataTableModel param)
        {
            var userID=User.UserDetailsID;
            //for sorting
            var sortDirection = Request["sSortDir_0"];//asc or desc
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);//Get Sorting Index of Column
            var dateFields = Request["sSearch_2"];//Get Sorting Index of Column
            int iDisplayLength = param.iDisplayLength;
                #region to call GetTransectionDetailsByUserID
            //Create URL
            string url = BaseUtility.GetApiUrl("UserApi", "GetUsersByAdminID");
            url = string.Format(url + "?adminID={0}", userID);

            //Invoke API
            HttpResponseMessage messageTypeList = await BaseUtility.CallGetAPI(url);
            List<GetUsersByAdminID_Result> result = null;
            //Check API response
            if (messageTypeList.IsSuccessStatusCode)
            {
                var jsonString = await messageTypeList.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<GetUsersByAdminID_Result>>(jsonString);
            }
            #endregion
         // var response = 
              //concatAllEquipment.OrderBy(x => x.IsSignIn).ThenBy(x => x.ItemDate);
            //IEnumerable<string[]> response = from c in result
            //                                 select new[] { Convert.ToString(c.Equipment), string.Format("{0:d}", c.ItemDate), c.Job, (c.IsSignIn == false ? "false" : "true"), null, null, c.EquipmentClass };
            ////c.EquipmentClass
            
            var response = result.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = result.Count,
                iTotalDisplayRecords = result.Count,
                aaData = response,

            }, JsonRequestBehavior.AllowGet);
        }

    }
}
