﻿using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.DTO
{
    public class ProductDto:BaseDto
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Stock { get; set; } 

        public List<Sale>? Sales { get; set; }
    }
}
