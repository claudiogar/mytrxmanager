using System;
namespace api.Models.DbModels
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Recipient { get; set; }
        public string Currency { get; set; }
        public ProductType ProductType { get; set; }

        public Transaction()
        {
        }
    }
}
