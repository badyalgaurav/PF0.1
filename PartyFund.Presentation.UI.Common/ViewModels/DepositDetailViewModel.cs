using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
   public class DepositDetailViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<decimal> TotalSpendMoney { get; set; }
        public Nullable<System.DateTime> DepositDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }

       
    }
}
