using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Entities.Expense.Entity
{
    [Table("ExpenseGroupStatus")]
    public class ExpenseGroupStatus
    {
        public ExpenseGroupStatus()
        {
            ExpenseGroups = new HashSet<ExpenseGroup>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }uuuuuuuu
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<ExpenseGroup> ExpenseGroups { get; set; }
        
    }
}
