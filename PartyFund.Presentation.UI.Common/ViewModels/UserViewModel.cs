using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
   public class UserViewModel
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Salt { get; set; }
        public int UserDetailsID { get; set; }
    
    }
}
