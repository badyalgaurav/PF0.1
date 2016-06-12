using PartyFund.Presentation.UI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Unfortunatly its not working :(
namespace PartyFund.Infrastructure
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }
}