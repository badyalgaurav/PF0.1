using PartyFund.Services.Services;
using PartyFund.Services.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using PartyFund.DataAccess.Implementation.Repositories;
using PartyFund.DataAccess.Implementation.Abstraction;
using PartyFund.DataContracts.DataModel;
using System.Configuration;
using PartyFund.Presentation.UI.Common.ViewModels;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TransectionDetailsAPIController : ApiController
    {
        
        ITransectionDetailsRepository   transectionDetailsRepository = null;
        #region Constructor
        public TransectionDetailsAPIController()
        {
            transectionDetailsRepository = new TransectionDetailsRepository();
             
        }
        #endregion

        #region GetTransectionDetailsByUserID
        [AcceptVerbs("GET")]
        [ActionName("GetTransectionDetailsByUserID")]
        public HttpResponseMessage GetTransectionDetailsByUserID(string userID)
        {
            TransectionDetailViewModel response = transectionDetailsRepository.GetByUserID(userID);

            if (response == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion
          #region GetTransectionDetailsByUserID
        [AcceptVerbs("POST")]
        [ActionName("InsertTransectionRecord")]
        public HttpResponseMessage InsertTransectionRecord(TransectionDetailViewModel obj)
        {
             transectionDetailsRepository.Insert(obj);

            //if (response == null)
            //{
            //    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            //}
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
          #endregion
    }
}
