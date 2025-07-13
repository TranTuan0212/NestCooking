using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models.Admin
{
    public class UserStatistic
    {
        [Key]
        public DateOnly Date { get; set; }
        public int NumberOfNewUser { get; set; } = 0;
        public int TotalOfUser { get; set; } = 0;
    }
}
