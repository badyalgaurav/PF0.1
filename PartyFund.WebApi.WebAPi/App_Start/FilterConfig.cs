using System.Web;
using System.Web.Mvc;

namespace PartyFund.WebApi.WebAPi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
