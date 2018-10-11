using AssetMgmt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AssetMgmt.DAL
{
    public class AssetMgmtContext : DbContext
    {
        public AssetMgmtContext(): base("AssetMgmtContext")
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetManagement> AssetManagements { get; set; }

        //removes plural
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}