﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models.DbModels;
using api.Utilities;

namespace api.Models.ApiModels
{
    public class TransactionApiModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Recipient { get; set; }

        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        [AllowedCurrency]
        public string Currency { get; set; }

        [Required]
        [EnumDataType(typeof(ProductType))]
        public string ProductType { get; set; }

        public TransactionApiModel()
        {
        }
    }

    public class GetTransactionsResponseMessage
    {
        public string PreviousPageUrl { get; set; }
        public string NextPageUrl { get; set; }

        public IEnumerable<TransactionApiModel> Transactions { get; set; } = new List<TransactionApiModel>();
    }
}
