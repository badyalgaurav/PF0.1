using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private DBContext db = new DBContext();

        // GET api/Employee
        public IEnumerable<EmployeesDetailsModel> GetEmployeesDetailsModels()
        {
            return db.EmployeesDetailsModels.AsEnumerable();
        }

        // GET api/Employee/5
        public EmployeesDetailsModel GetEmployeesDetailsModel(int id)
        {
            EmployeesDetailsModel employeesdetailsmodel = db.EmployeesDetailsModels.Find(id);
            if (employeesdetailsmodel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return employeesdetailsmodel;
        }

        // PUT api/Employee/5
        public HttpResponseMessage PutEmployeesDetailsModel(int id, EmployeesDetailsModel employeesdetailsmodel)
        {
            if (ModelState.IsValid && id == employeesdetailsmodel.ID)
            {
                db.Entry(employeesdetailsmodel).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Employee
        public HttpResponseMessage PostEmployeesDetailsModel(EmployeesDetailsModel employeesdetailsmodel)
        {
            if (ModelState.IsValid)
            {
                db.EmployeesDetailsModels.Add(employeesdetailsmodel);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employeesdetailsmodel);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = employeesdetailsmodel.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Employee/5
        public HttpResponseMessage DeleteEmployeesDetailsModel(int id)
        {
            EmployeesDetailsModel employeesdetailsmodel = db.EmployeesDetailsModels.Find(id);
            if (employeesdetailsmodel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.EmployeesDetailsModels.Remove(employeesdetailsmodel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, employeesdetailsmodel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}