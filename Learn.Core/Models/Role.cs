﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Models
{
    public class Role
    {
        public string Name { get; set; }

        public ICollection<GroupInRole>GroupInRoles { get; set; }
    }
}
