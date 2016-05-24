using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.ViewModels
{
  public  class CustomPrincipalSerializeModel
    {
      public int ID { get; set; }
      public string UserName { get; set; }
      public string Email { get; set; }
      public string PlainTextPassword { get; set; }
      public int RoleId { get; set; }

    }
}
