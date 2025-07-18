﻿using NESTCOOKING_API.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Repositories.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<string> GetRoleAsync(string userId);
        bool IsUniqueEmail(string email);
        bool IsUniqueUserName(string username);
        Task<User> Login(string username, string password);
        Task<bool> Register(User newUser, string password);
        Task<IEnumerable<User>> GetUsersByCriteriaAsync(string criteria, string? userId = null);
        Task<User> FindUserByRoleIdAndUserName(string roleId, string userName);
        Task IncreaseUserBalanceAsync(string userId, double amount);
    }
}
