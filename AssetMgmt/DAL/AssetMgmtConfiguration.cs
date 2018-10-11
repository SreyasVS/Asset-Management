using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace AssetMgmt.DAL
{
    public class AssetMgmtConfiguration : DbConfiguration
    {
        public AssetMgmtConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}