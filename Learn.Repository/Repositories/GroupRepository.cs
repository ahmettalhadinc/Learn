﻿using Learn.Core.Models;
using Learn.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repository.Repositories
{
    public class GroupRepository(AppDbContext context) : GenericRepository<Group>(context), IGroupRepository
    {
    }
}
