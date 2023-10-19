using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Accounts
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;
        public DateTime Last_modified_date { get; set; }
        [ForeignKey("User")]
        public Guid Id_User { get; set; }
        public virtual User? User { get; set; }

    }
}
