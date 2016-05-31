//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.Cors;

//namespace WebApi.Controllers
//{
//    [EnableCors(origins: "*", headers: "*", methods: "*")]
//    public class StudentController : ApiController
//    {
//        private WebApiPracticeEntities1 db = new WebApiPracticeEntities1();

//        // GET api/Student
//        [HttpGet]
//       // [EnableCors(origins: "*", headers: "Access-Control-Allow-Origin", methods: "*")]
//        public IEnumerable<EmployeeDetail> GetEmployeeDetails()
//        {

//            var records = db.EmployeeDetails.AsEnumerable();
//            var result = records;

//            return db.EmployeeDetails.AsEnumerable();
//        }

//        // GET api/Student/5
//        public EmployeeDetail GetEmployeeDetail(int id)
//        {
//            EmployeeDetail employeedetail = db.EmployeeDetails.Find(id);
//            if (employeedetail == null)
//            {
//                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
//            }

//            return employeedetail;
//        }

//        // PUT api/Student/5
//        [HttpPost]
//        public HttpResponseMessage PutEmployeeDetail(EmployeeDetail student)
//        {
//            if (student.ID != 0)
//            {
//                db.Entry(student).State = EntityState.Modified;

//                try
//                {
//                    db.SaveChanges();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    return Request.CreateResponse(HttpStatusCode.NotFound);
//                }

//                return Request.CreateResponse(HttpStatusCode.OK);
//            }
//            else
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest);
//            }
//        }

//        // POST api/Student
     
//        public HttpResponseMessage PostEmployeeDetail(EmployeeDetail student)
//        {
//            if (student.Name != null)
//            {
//                //if (ModelState.IsValid)
//                //{
//                    db.EmployeeDetails.Add(student);
//                    db.SaveChanges();

//                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, student);
//                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = student.ID }));
//                    return response;
//                //}
//            }
//            else
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest);
//            }
//        }

//        // DELETE api/Student/5
//        [HttpDelete]
//        public HttpResponseMessage DeleteEmployeeDetail(int id)
//        {
//            EmployeeDetail employeedetail = db.EmployeeDetails.Find(id);
//            if (employeedetail == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound);
//            }

//            db.EmployeeDetails.Remove(employeedetail);

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound);
//            }

//            return Request.CreateResponse(HttpStatusCode.OK, employeedetail);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}