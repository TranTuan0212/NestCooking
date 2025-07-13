﻿using NESTCOOKING_API.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.Business.Services.IServices
{
    public interface IUserConnectionService
    {
        Task<List<UserShortInfoDTO>> GetAllFollowingUsersByUserIdAsync(string userId);
        Task<List<UserShortInfoDTO>> GetAllFollowersByUserIdAsync(string userId);
        Task CreateUserConnectionAsync(string userId, string followingUserId);
        Task RemoveAllFollowingUsersByUserIdAsync(string userId);
        Task RemoveAllFollowersByUserIdAsync(string userId);
        Task RemoveUserConnectionAsync(string userId, string followingUserId);
    }
}
