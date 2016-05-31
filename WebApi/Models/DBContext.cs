using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace WebApi.Models
{
    public class DBContext : DbContext
    {
      //  public DbSet<EmployeeDetail> Employees { get; set; }

        public DbSet<EmployeesDetailsModel> EmployeesDetailsModels { get; set; }
    }
}