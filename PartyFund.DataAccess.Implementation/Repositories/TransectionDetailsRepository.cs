using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.DataAccess.Implementation.Abstraction;
using PartyFund.Presentation.UI.Common.ViewModels;
using PartyFund.DataContracts.DataModel;
using System.Data.Entity;
namespace PartyFund.DataAccess.Implementation.Repositories
{
    public class TransectionDetailsRepository : ITransectionDetailsRepository
    {
        private PartyFundEntities context = null;

        public TransectionDetailsRepository()
        {
            this.context = new PartyFundEntities();
        }


        public TransectionDetailsRepository(PartyFundEntities db)
        {
            this.context = db;
        }

        public IQueryable<TransectionDetail> GetAllUsers()
        {

            return context.TransectionDetails;
        }

        public IQueryable<TransectionDetail> GetByID(string id)
        {
            var Id = Convert.ToInt16(id);
            return context.TransectionDetails.Where(x => x.ID == Id);
        }

        #region GetByUserID
        /// <summary>
        /// this method will return all the transections by particular user
        /// for eg. when admin want to credit amount for a particular user or organization
        /// (THIS METHOD USED TO DISPLAY RECORD)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public TransectionDetailViewModel GetByUserID(string userID)
        {
            var userId = Convert.ToInt16(userID);
            var result = context.TransectionDetails.Where(x => x.UserID == userId).OrderByDescending(x => x.DateCreated).FirstOrDefault();
            var response = (from p in context.TransectionDetails
                            join
                                q in context.UserDetails on p.UserID equals q.ID
                            join r in context.Users on q.ID equals r.UserDetailsID
                            select new TransectionDetailViewModel { CurrentAmount = p.CurrentAmount, UserName = r.UserName, DateCreated = p.DateCreated,UserID = p.UserID }).OrderByDescending(x=>x.DateCreated).FirstOrDefault();
            return response;
        }
        #endregion

        #region Insert
        /// <summary>
        /// Insert new credit or debit process
        /// </summary>
        /// <param name="model"></param>
        public void Insert(TransectionDetailViewModel model)
        {

                var transectionDetails = new TransectionDetail { CurrentAmount = model.TotalAmount, TransectionAmount = model.TransectionAmount, Action = model.Action, DateCreated = DateTime.Now, CreatedBy = model.CreatedBy, UserID = model.UserID };
                context.TransectionDetails.Add(transectionDetails);
                Save();
            
           
        }
        #endregion

        #region Update
        /// <summary>
        /// to update (edit)
        /// </summary>
        /// <param name="obj"></param>
        public void Update(TransectionDetailViewModel obj)
        {
            //here might be need to convert it to transection detail 
            context.Entry(obj).State = EntityState.Modified;
        }
        #endregion

        #region Delete
        public void Delete(string id)
        {
            var ID = Convert.ToInt16(id);
            TransectionDetail existing = context.TransectionDetails.Where(x => x.ID == ID).FirstOrDefault();
            existing.IsDeleted = true;
        }
        #endregion

        public void InsertAdminAmount(List<TransectionDetail> model)
        {
            foreach (var item in model)
            {
                context.TransectionDetails.Add(item);
            }
            Save();
            
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        TransectionDetail ITransectionDetailsRepository.GetByID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
