using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderDetails
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int Status { get; set; }
        [ForeignKey("Order")]
        public Guid Id_order { get; set; }
        [ForeignKey("Product_details")]

        public Guid Id_productDetails { get; set; }
        public virtual ProductDetail ?  ProductDetail { get; set; }
        public virtual Order? Order { get; set; }
    }
}
