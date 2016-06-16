using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
   public class TransectionDetailViewModel
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<decimal> CurrentAmount { get; set; }
        public Nullable<decimal> TransectionAmount { get; set; }
        public string Action { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string Notes { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
       
       //refrence from userviewmodel
        public string UserName { get; set; }

      
    }
}
