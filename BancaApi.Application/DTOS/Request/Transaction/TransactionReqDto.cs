using BancaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Request.Transaction
{
    public class TransactionReqDto
    {
        [Required(ErrorMessage = "AccountNumber is required.")]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
    }
}
