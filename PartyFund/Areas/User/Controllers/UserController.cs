using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyFund.Presentation.UI.Common.ViewModels;
using PartyFund.Services.Services.Abstraction;
using PartyFund.Services.Services;
using PartyFund.Presentation.UI.Common;

namespace PartyFund.Areas.User.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/User/

        IUserDetailsServices iUserDetailsServices = null;
        public UserController()
        {
            iUserDetailsServices = new UserDetailsServices();
        }
        public ActionResult Index()
        {
            return View();
        }
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
