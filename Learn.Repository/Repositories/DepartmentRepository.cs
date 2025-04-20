using Learn.Core.Models;
using Learn.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repository.Repositories
{
    public class DepartmentRepository(AppDbContext context):GenericRepository<Department>(context),IDepartmentRepository
    {
    }
}
