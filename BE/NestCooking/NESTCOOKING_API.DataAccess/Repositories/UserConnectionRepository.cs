﻿using NESTCOOKING_API.DataAccess.Data;
using NESTCOOKING_API.DataAccess.Models;
using NESTCOOKING_API.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Repositories
{
    public class UserConnectionRepository : Repository<UserConnection>, IUserConnectionRepository
    {
        public UserConnectionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
