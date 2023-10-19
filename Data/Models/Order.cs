﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Account")]
        public Guid Id_Account { get; set; }
        public string Note { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float TotalMoney { get; set; }
        public float Transportfee { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Last_modified_date { get; set; }
        public int Status { get; set; }

        public virtual Accounts? Account { get; set; }
    }
}
