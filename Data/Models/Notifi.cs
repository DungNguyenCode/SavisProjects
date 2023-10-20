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
    public class Notifi
    {
        [Key]
        public Guid Id { get; set; }
        public string Noti_conten { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime Last_modified_date { get; set; }
        [ForeignKey("Account")]
        public Guid Id_account { get; set; }
        public virtual Accounts? Account { get; set; }
    }
}
