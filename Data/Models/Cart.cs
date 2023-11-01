using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Create_at { get; set; }
        [ForeignKey("User")]
        public Guid Id_User { get; set; }
        public virtual User? User { get; set; }
    }
}
