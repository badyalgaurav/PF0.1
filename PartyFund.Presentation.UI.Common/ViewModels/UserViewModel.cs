using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
   public class UserViewModel
    {
        public int ID { get; set; }
       [DisplayName("Email Address")]
        public string Email { get; set; }
       [DisplayName("User Name")]
        public string UserName { get; set; }
       [DisplayName("Password")]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Salt { get; set; }
        public int UserDetailsID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
