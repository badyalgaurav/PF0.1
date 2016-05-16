using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
  public class UserDetailViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CompanyName { get; set; }
        public string Desgination { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int ParentID { get; set; }
    }
}
