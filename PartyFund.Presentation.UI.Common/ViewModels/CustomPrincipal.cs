using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
    public class CustomPrincipal : IPrincipal

    {
        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            if (role.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
        public int ID { get; set; }
        public int? UserDetailsID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PlainTextPassword { get; set; }
        public int RoleId { get; set; }
        public string CompanyName { get; set; }
        public string[] roles { get; set; }
    }
}
