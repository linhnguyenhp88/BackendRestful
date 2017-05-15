using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Entities
{
    public class Ftag
    {    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("Ftag")]
        public string ftag { get; set; }
        [Column("INPTYPE")]
        [Required]
        public string inptype { get; set; }
        [Column("CAPTION")]
        [Required]
        public string caption { get; set; }
        [Column("REQUIRE")]
        [Required]
        public int require { get; set; }
    }
}
