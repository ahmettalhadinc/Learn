using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.DTO.UpdateDTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string name { get; set; }

        public int DepartmentId { get; set; }
        public int GroupId { get; set; }

    }
}
