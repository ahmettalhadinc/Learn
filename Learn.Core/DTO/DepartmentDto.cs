﻿using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.DTO
{
    public class DepartmentDto:BaseDto
    {
        public string Name { get; set; }

        public List<User>? Users { get; set; }
    }
}
