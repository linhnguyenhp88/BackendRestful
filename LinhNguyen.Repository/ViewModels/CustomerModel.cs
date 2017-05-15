using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.ViewModels
{
    public class CustomerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("UserName")]
        public virtual string Username { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Password")]
        public virtual string Password { get; set; }
    }
}
