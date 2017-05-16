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
        public virtual DbSet<Entities.Expense.Entity.Expense> Expenses { get; set; }
        public virtual DbSet<Entities.Expense.Entity.ExpenseGroup> ExpenseGroups { get; set; }
        public virtual DbSet<Entities.Expense.Entity.ExpenseGroupStatus> ExpenseGroupStatuses { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Expense.Entity.Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Entities.Expense.Entity.ExpenseGroup>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.ExpenseGroup).WillCascadeOnDelete();

            modelBuilder.Entity<Entities.Expense.Entity.ExpenseGroupStatus>()
                .HasMany(e => e.ExpenseGroups)
                .WithRequired(e => e.ExpenseGroupStatus)
                .HasForeignKey(e => e.ExpenseGroupStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}
