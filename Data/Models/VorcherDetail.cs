using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class VorcherDetail
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Order")]
        public Guid Id_order { get; set; }
        [ForeignKey("Voucher")]
        public Guid Id_Voucher { get; set; }
        public float BeforPrice { get; set; }
        public float AfterPrice { get; set; }
        public float DiscountPrice { get; set; }


        public virtual Vorcher? Voucher { get; set; }
        public virtual Order? Order { get; set; }
    }
}
