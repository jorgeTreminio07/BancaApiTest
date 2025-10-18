using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Request.BankAccount
{
    public class BankAccountReqDto
    {
        [Required(ErrorMessage = "ClientId is required.")]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "InitialBalance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Initial balance cannot be negative.")]
        public decimal InitialBalance { get; set; }
    }
}
