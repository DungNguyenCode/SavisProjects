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
        [ForeignKey("Account")]
        public Guid Id_account { get; set; }
        public virtual Accounts? Account { get; set; }
    }
}
