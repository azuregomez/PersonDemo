﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonDemoWeb.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.Entity.Infrastructure;
    
    public partial class persondemoEntities : DbContext
    {
        /// <summary>
        /// This constructor was handcoded in order to inject the cnString in code.        
        /// </summary>
        /// <param name="cnString"></param>
        public persondemoEntities(string cnString) : base(new EntityConnection(new EntityConnectionStringBuilder()
        {
            Provider = "System.Data.SqlClient",
            ProviderConnectionString = cnString,
            Metadata = "res://*/EF.PersonModel.csdl|res://*/EF.PersonModel.ssdl|res://*/EF.PersonModel.msl"
        }.ToString()), true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Person> People { get; set; }
    }
}
