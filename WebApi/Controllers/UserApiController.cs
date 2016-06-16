using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Http.Cors;
using PartyFund.Services.Services;
using PartyFund.Services.Services.Abstraction;
using PartyFund.Presentation.UI.Common.ViewModels;
using PartyFund.DataContracts.DataModel;
using Newtonsoft.Json;
using System.Web.Http.Description;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserApiController : ApiController
    {
        IUserDetailsServices iUserDetailsServices = null;
        IUserServices iUserServices = null;
        #region Constructor
        public UserApiController()
        {
            iUserDetailsServices = new UserDetailsServices();
            iUserServices = new UserServices();
             
        }
        #endregion

        #region To Register a new user
        /// <summary>
        /// RegisterApi to register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns> return success or failure message</returns>
        [HttpPost]
        public HttpResponseMessage RegisterApi(UserDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                iUserDetailsServices.Insert(model);
                iUserServices.Insert(model);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = model.ID }));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        #endregion

        #region GetUsersByAdminID (Get user list by admin ID)
        /// <summary>
        /// GetUsersByAdminID
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns>return list of users added by admin </returns>
        [HttpGet]
        public IEnumerable<UserDetail> GetUsersByAdminID(string adminID)
        {
            List<UserDetail> response = iUserDetailsServices.GetByAdminID(adminID).ToList();
            
            if (response == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return response;
        }
        #endregion

        #region GetByID( To get a single user record)
        // GET GetByID
        [ResponseType(typeof(UserDetail))]
        public IHttpActionResult GetByID(string id)
        {
            return Ok(iUserDetailsServices.GetByID(id));
        }
        #endregion
    }
}
