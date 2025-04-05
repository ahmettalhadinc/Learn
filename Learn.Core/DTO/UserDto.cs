﻿using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.DTO
{
    public class UserDto
    {
        public string name { get; set; }

        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
  

        public Department Department { get; set; }
        public Group Group { get; set; }
    }
}
