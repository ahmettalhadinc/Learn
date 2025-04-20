using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.DTO.UpdateDTOs
{
    public class GroupInRoleUpdateDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
    }
}
