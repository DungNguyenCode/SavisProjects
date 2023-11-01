using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string ?Avatar { get; set; }
        public string ?Email { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime Last_modified_date { get; set; }
        [ForeignKey("Role")]
        public Guid  Id_Role { get; set; }
        public virtual ICollection<Role>? Roles { get; set; }
    }
}
