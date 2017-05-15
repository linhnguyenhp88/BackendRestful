using LinhNguyen.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.DAL
{
    public partial class SraContext : DbContext
    {
        static SraContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SraContext>());
        }
        public SraContext(): base(nameof(SraContext))
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Ftag> Ftags { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
