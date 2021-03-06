﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PartyFund.DataContracts.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PartyFundEntities : DbContext
    {
        public PartyFundEntities()
            : base("name=PartyFundEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DepositDetail> DepositDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TransectionDetail> TransectionDetails { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserInRole> UserInRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<GetUsersByAdminID_Result2> GetUsersByAdminID(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUsersByAdminID_Result2>("GetUsersByAdminID", userIdParameter);
        }
    }
}
