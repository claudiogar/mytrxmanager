
using System;
using api.Models.ApiModels;
using api.Models.DbModels;

namespace api.Models
{
    internal static class EntityTransformer
    {
        public static TransactionDbModel ToDbModel(this TransactionApiModel trx)
        {
            return new TransactionDbModel
            {
                Id = trx.Id,
                Amount = trx.Amount,
                Currency = trx.Currency,
                Date = trx.Date,
                ProductType = (ProductType) Enum.Parse(typeof(ProductType), trx.ProductType),
                Recipient = trx.Recipient.Trim()
            };
        }

        public static TransactionApiModel ToApiModel(this TransactionDbModel trx)
        {
            return new TransactionApiModel
            {
                Id = trx.Id,
                Amount = trx.Amount,
                Currency = trx.Currency,
                Date = trx.Date,
                ProductType = Enum.GetName(trx.ProductType),
                Recipient = trx.Recipient
            };
        }
    }
}
