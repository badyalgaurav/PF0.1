﻿using PartyFund.Presentation.UI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Services.Services.Abstraction
{
  public  interface IUserServices
    {
      void Insert(UserDetailViewModel obj);
    }
}
