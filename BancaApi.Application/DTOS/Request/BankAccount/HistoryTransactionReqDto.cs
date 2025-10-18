using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Request.BankAccount
{
    public class HistoryTransactionReqDto
    {
        [Required]
        public required string AccountNumber { get; set; } 
    }
}
