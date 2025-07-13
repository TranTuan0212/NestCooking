using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models.Admin
{
    public class ChefStatistic
    {
        [Key]
        public DateOnly Date { get; set; }
        public int NumberOfNewChef { get; set; } = 0;
        public int TotalOfChef { get; set; } = 0;
    }
}
