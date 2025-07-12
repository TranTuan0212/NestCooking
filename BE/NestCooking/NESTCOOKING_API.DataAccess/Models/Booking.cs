﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class Booking
    {
        [Key]
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string ChefId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public double Total { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime ApprovalStatusDate { get; set; }
        public string PhoneNumber { get; set; } = null!;

        // One booking can have many transactions. Example: take money of user, give chef money,...
        public required List<string> TransactionIdList { get; set; }
    }
}
