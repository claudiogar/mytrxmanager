using System;
namespace api.Models.ApiModels
{
    public class TransactionApiModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Recipient { get; set; }
        public string Currency { get; set; }
        public string ProductType { get; set; }

        public TransactionApiModel()
        {
        }
    }
}
