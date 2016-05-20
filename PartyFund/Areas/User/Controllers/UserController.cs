using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyFund.Areas.User.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/User/

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
        public ActionResult Registration(string abc)
        {
            return View();
        }

    }
}
