﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedData { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public bool Status { get; set; }

    }
}
