using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string ?District { get; set; }
        public string ?City { get; set; }
        public string ?Wards { get; set; }
        public string ?Detailed_address { get; set; }
        [ForeignKey("User")]
        public Guid Id_User { get; set; }
        public virtual User? User { get; set; }
    }
}
