using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Entities
{
    public class Menu
    {      
        [Key]
        [Column("CMDID")]
        public string cmdid { get; set; }

        [Required]
        [Column("CAPTION")]
        public string caption { get; set; }

        [Required]
        [Column("CMDTYPE")]
        public string cmdtype { get; set; }
        [Required]
        [Column("PARENTID")]
        public string parentid { get; set; }
    }
}
