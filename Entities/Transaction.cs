using System.ComponentModel.DataAnnotations;

namespace MoneyManagement.API.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public double Amount { get; set; }

        public string Type { get; set; }

        public string Username { get; set; }
    }
}