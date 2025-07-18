﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsMale { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public double? BookingPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public double Balance { get; set; } = 0;
        public string RoleId { get; set; } = null!;

        [JsonIgnore]
        public List<RequestToBecomeChef> RequestsToBecomeChefs { get; set; }
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }
        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; }
    }
}
