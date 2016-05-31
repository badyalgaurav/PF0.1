using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;



namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
      //  private readonly WebApiPracticeEntities1 context = new WebApiPracticeEntities1();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "Value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "You type Id = " + id;
        }

        // POST api/values
        [HttpPost]
        public string Post(EmployeesDetailsModel model)
        {
            //EmployeeDetail empDetails = new EmployeeDetail();
            //if (model != null)
            //{
            //    empDetails.Email = model.Email.Trim();
            //    empDetails.Name = model.Name.Trim();
            //    empDetails.Age = model.Age.Trim();
            //    empDetails.City = model.City.Trim();
            //    context.EmployeeDetails.Add(empDetails);
            //    context.SaveChanges();
            //}

            return "ok";
        }

        // PUT api/values/5
        public IEnumerable<string> Put(int Id)
        {
            return new string[] { "test1", "test2" };
        }

        // DELETE api/values/5
        [HttpGet]
        public int Delete(int Id)
        {
            return 1;
        }

    }
}