using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.DAL
{
    public class SraCRMConfiguration :  DbConfiguration
    {
        public SraCRMConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new InterceptorTransientErrors());
            DbInterception.Add(new InterceptorLogging());
        }
    }
}
