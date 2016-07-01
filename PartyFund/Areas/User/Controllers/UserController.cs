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
        public async Task<ActionResult> Index()
        {
            decimal? sum = 0;
            var adminID = User.UserDetailsID;
            List<GetUsersByAdminID_Result2> userList = new List<GetUsersByAdminID_Result2>();
            #region to call GetTransectionDetailsByUserID
            //Create URL
            string url = BaseUtility.GetApiUrl("UserApi", "GetUsersByAdminID");
            url = string.Format(url + "?adminID={0}", adminID);
            //Invoke API
            HttpResponseMessage messageTypeList = await BaseUtility.CallGetAPI(url);
            //Check API response
            if (messageTypeList.IsSuccessStatusCode)
            {
                var jsonString = await messageTypeList.Content.ReadAsStringAsync();
                userList = JsonConvert.DeserializeObject<List<GetUsersByAdminID_Result2>>(jsonString);
            }
            //this query will return one record of a single user order by record.datecreated
            var otherQuery = userList.GroupBy(record => new { record.ID }).Select(g => g.OrderByDescending(record => record.DateCreated).FirstOrDefault()).ToList();
            System.Web.HttpContext.Current.Cache["userList"] = otherQuery;
            foreach (var item in otherQuery)
            {
                var itemAmount = item.CurrentAmount == null ? 0 : item.CurrentAmount;
                sum = sum + itemAmount;
            }
            //string format C0 to format to dollar sign , G29 for simple
            var stringSum = String.Format("{0:C0}", sum);
            ViewBag.organicationAmount = stringSum;
            ViewBag.CompanyName = User.CompanyName;
            #endregion
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
                var id = User.UserDetailsID;
                //this is used to send request to web api to get record for a particular user
                string url = BaseUtility.GetApiUrl("UserApi", "GetByID");
                url = string.Format(url + "?id={0}", id);
                HttpResponseMessage responseMessage = await BaseUtility.CallGetAPI(url);
                //HttpResponseMessage responseMessage = await client.GetAsync(url + "GetByID/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<UserDetail>(responseData);
                    //here viewbag is used to display company name in readonly mode
                    ViewBag.CompanyName = result.CompanyName;
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(UserDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? parentId = User.UserDetailsID;
                    var password = model.Password;
                    var PASSWORD_BCRYPT_COST = 8; // work factor
                    var PASSWORD_SALT = Utility.GetSalt(); //generated random salt
                    var salt = "$2a$" + PASSWORD_BCRYPT_COST + "$" + PASSWORD_SALT;
                    var pwdToHash = password + salt;
                    var hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt());
                    model.ParentID = Convert.ToInt16(parentId);
                    model.Password = hashToStoreInDatabase;
                    model.Salt = PASSWORD_SALT;
                    ViewBag.companyName = model.CompanyName;
                    string url = BaseUtility.GetApiUrl("UserApi", "RegisterApi");
                    HttpResponseMessage responseMessage = await BaseUtility.CallPostAPI(url, model);
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
                    ModelState.AddModelError("", PartyFund.Controllers.AccountController.ErrorCodeToString(e.StatusCode));
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

            var userID = id;
            TransectionDetailViewModel transectionDetails;
            decimal? sum = 0;
            // TransectionDetailViewModel transectionDetails = new TransectionDetailViewModel();
            //for organization
            if (id == "0")
            {
                userID = Convert.ToString(User.UserDetailsID);

                var userList = (List<GetUsersByAdminID_Result2>)System.Web.HttpContext.Current.Cache["userList"];

                foreach (var item in userList)
                {
                    var itemAmount = item.CurrentAmount == null ? 0 : item.CurrentAmount;
                    sum = sum + itemAmount;
                }
                //here username is using as a organization Name
                transectionDetails = new TransectionDetailViewModel { UserName = User.CompanyName, CurrentAmount = sum, UserID = Convert.ToInt16(id) };
            }
            else
            {
                //for user
                transectionDetails = await GetTransectionDetailsByUserID(userID);
            }
            if (param == "add")
            {
                transectionDetails.Action = "C";
                ViewBag.sign = "+";
                ViewBag.action = "Credit Amount";
            }
            else
            {
                transectionDetails.Action = "D";
                ViewBag.sign = "-";
                ViewBag.action = "Debit Amount";
            }
            return View("~/Areas/User/Views/User/_CreditDebitUserAmount.cshtml", transectionDetails);
        }
        #endregion

        #region METHOD to get transection details to modal popup (ADD - Subtract operation)
        public async Task<TransectionDetailViewModel> GetTransectionDetailsByUserID(string userID)
        {
            TransectionDetailViewModel transectionDetails = new TransectionDetailViewModel();
            #region to call GetTransectionDetailsByUserID
            string url = BaseUtility.GetApiUrl("TransectionDetailsAPI", "GetTransectionDetailsByUserID");
            url = string.Format(url + "?userID={0}", userID);
            HttpResponseMessage messageTypeList = await BaseUtility.CallGetAPI(url);
            if (messageTypeList.IsSuccessStatusCode)
            {
                var jsonString = await messageTypeList.Content.ReadAsStringAsync();
                transectionDetails = JsonConvert.DeserializeObject<TransectionDetailViewModel>(jsonString);
            }
            #endregion
            return transectionDetails;
        }
        #endregion

        #region to Get UserList in DataTable
        public JsonResult GetUsersByAdminIdDT(JqueryDataTableModel param)
        {
            var userID = User.UserDetailsID;
            //for sorting
            var sortDirection = Request["sSortDir_0"];//asc or desc
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);//Get Sorting Index of Column
            var dateFields = Request["sSearch_2"];//Get Sorting Index of Column
            int iDisplayLength = param.iDisplayLength;
            //userlist cache is created in INDEX method
            var userList = (List<GetUsersByAdminID_Result2>)System.Web.HttpContext.Current.Cache["userList"];
            var response = userList.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = userList.Count,
                iTotalDisplayRecords = userList.Count,
                aaData = response,

            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public async Task<ActionResult> UpdateOrganizationAmount(TransectionDetailViewModel model)
        {
            List<TransectionDetail> transectionDetailsList = new List<TransectionDetail>();
            var userList = ((List<GetUsersByAdminID_Result2>)System.Web.HttpContext.Current.Cache["userList"]);
            var numberOfUser = userList.Count;
            var meanAmount = Convert.ToDecimal(model.TransectionAmount) / Convert.ToDecimal(numberOfUser);
            foreach (var item in userList)
            {
                transectionDetailsList.Add(new TransectionDetail { CurrentAmount = (item.CurrentAmount == null ? 0 : item.CurrentAmount) + meanAmount, Action = model.Action, DateCreated = DateTime.Now, CreatedBy = User.UserName, TransectionAmount = meanAmount, UserID = item.ID });

            }

            #region to call GetTransectionDetailsByUserID
            string url = BaseUtility.GetApiUrl("TransectionDetailsAPI", "InsertAdminAmount");
            //  url = string.Format(url + "?userID={0}", model.UserID);
            HttpResponseMessage messageTypeList = await BaseUtility.CallListPostAPI(url, transectionDetailsList);
            if (messageTypeList.IsSuccessStatusCode)
            {
                var jsonString = await messageTypeList.Content.ReadAsStringAsync();
                // transectionDetails = JsonConvert.DeserializeObject<TransectionDetailViewModel>(jsonString);
            }
            #endregion

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
