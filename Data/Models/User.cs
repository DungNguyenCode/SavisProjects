using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string? Fullname { get; set; }
        public string ?PhoneNumber { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string ?Email { get; set; }
        public int Gender { get; set; }
        public float Point { get; set; }
        public int Status { get; set; }

        public virtual Accounts? Account { get; set; }
    }
}
