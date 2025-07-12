using Microsoft.AspNetCore.Identity;
using NESTCOOKING_API.DataAccess.Data;
using NESTCOOKING_API.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Repositories
{
    public class RoleRepository : Repository<IdentityRole>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> GetRoleNameByIdAsync(string roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (role == null)
            {
                return null;
            }
            return role.Name;
        }

        public async Task<string> GetRoleIdByNameAsync(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                return null;
            }
            return role.Id;
        }

        public async Task<bool> ChangeRoleAsync(string username, string roleName)
        {
            var user = _context.Users.FirstOrDefaultAsync(u => u.UserName == username).Result;
            if (user == null)
            {
                return false;
            }

            var roleId = await GetRoleIdByNameAsync(roleName);
            if (roleId == null)
            {
                return false;
            }
            user.RoleId = roleId;
            _context.Users.Update(user);
            await SaveAsync();

            return true;
        }
    }
}
