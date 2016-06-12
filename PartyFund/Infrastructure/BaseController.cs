﻿using PartyFund.Presentation.UI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyFund.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

    }
}
