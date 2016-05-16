using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyFund.DataContracts.DataModel;
using PartyFund.DataAccess.Implementation.Abstraction;
using System.Data.Entity;
namespace PartyFund.DataAccess.Implementation.Repositories
{
  public class UserRepository :BaseRepository<User>, IUserRepository
    {
      public UserRepository(DbContext context) : base(context) { }
    }
}
