using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.DTO
{
    public class UserDto:BaseDto
    {
        public string name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
  

        public Department? Department { get; set; }
        public Group? Group { get; set; }
    }
}
