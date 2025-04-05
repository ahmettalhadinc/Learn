using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Models
{
    public class User : BaseEntity
    {

        public string name { get; set; }

        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PassWordHash{ get; set; }

        public Department Department { get; set; }
        public Group Group { get; set; }
    }
}
