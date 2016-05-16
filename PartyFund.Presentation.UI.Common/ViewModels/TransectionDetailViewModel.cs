﻿using System;
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
        public string CurrentAmount { get; set; }
        public Nullable<int> WithDraws { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Notes { get; set; }
    }
}
