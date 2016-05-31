﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class EmployeesDetailsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Email { get; set; }
    }
}