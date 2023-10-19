﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Material
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }= DateTime.Now;
        public DateTime Last_modified_date { get; set; }
        public int Status { get; set; }
    }
}
