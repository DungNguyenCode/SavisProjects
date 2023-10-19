using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PaymentMethod
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Order")]
        public Guid Id_order { get; set; }
        public int Method { get; set; }
        public string ?Description { get; set; }
        public float TotalMoney { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Last_modified_date { get; set; }
        public virtual Order? Order { get; set; }
    }
}
