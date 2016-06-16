using PartyFund.DataContracts.DataModel;
using PartyFund.Presentation.UI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.DataAccess.Implementation.Abstraction
{
    public interface ITransectionDetailsRepository
    {
        IQueryable<TransectionDetail> GetAllUsers();
        TransectionDetailViewModel GetByUserID(string userID);
        TransectionDetail GetByID(string id);
        void Insert(TransectionDetailViewModel obj);
        void Update(TransectionDetailViewModel obj);
        void Delete(string id);
        void Save();

    }
}
