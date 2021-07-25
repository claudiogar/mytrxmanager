
using System;
using System.Collections.Generic;
using System.Linq;
using api.Models.ApiModels;
using api.Models.DbModels;

namespace api.Models
{

    public static class EntityTransformer
    {
        private static IReadOnlyDictionary<string, ProductType> ProductTypesMap = InitializeProductTypesMap();

        private static IReadOnlyDictionary<string, ProductType> InitializeProductTypesMap()
        {
            ProductType[] productTypes = Enum.GetValues<ProductType>();

            return productTypes.ToDictionary(p => Enum.GetName(p), p => p);
        }

        public static TransactionDbModel ToDbModel(this TransactionApiModel trx)
        {
            return new TransactionDbModel
            {
                Id = trx.Id,
                Amount = trx.Amount,
                Currency = trx.Currency,
                Date = trx.Date,
                ProductType = ProductTypesMap[trx.ProductType],
                Recipient = trx.Recipient
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
