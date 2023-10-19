using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CartDetails
    {
        [Key]
        public Guid ID { get; set; }
        [ForeignKey("Product_details")]
        public Guid Id_productdetails { get; set; }
        [ForeignKey("Cart")]
        public Guid Id_Cart { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Last_modified_date { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual ProductDetail?  ProductDetails { get; set; }
    }
}
