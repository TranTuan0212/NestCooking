using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class PurchasedRecipe
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
