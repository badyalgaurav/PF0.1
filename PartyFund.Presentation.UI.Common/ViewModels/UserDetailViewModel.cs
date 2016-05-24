using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
  public class UserDetailViewModel : UserViewModel
    {
        public int ID { get; set; }
       [DisplayName("First Name")]
        public string FirstName { get; set; }
       [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
       [DisplayName("Last Name")]
        public string LastName { get; set; }
       [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }
       [DisplayName("Street Address")]
        public string Address { get; set; }
       [DisplayName("City ")]
        public string City { get; set; }
       [DisplayName("State/Province")]
        public string State { get; set; }
       [DisplayName("Country")]
        public string Country { get; set; }
       [DisplayName("Organization Name")]
        public string CompanyName { get; set; }
       [DisplayName("Desgination")]
        public string Desgination { get; set; }
       [DisplayName("Department")]
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
