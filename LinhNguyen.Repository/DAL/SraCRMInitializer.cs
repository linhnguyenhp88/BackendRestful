using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.DAL
{
    public class SraCRMInitializer : DropCreateDatabaseIfModelChanges<SraContext>
    {
        protected override void Seed(SraContext context)
        {
            context.SaveChanges();
        }
    }
}
