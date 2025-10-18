using BancaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Request.Client
{
    public class ClientReqDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't exceed 100 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public required SexType Sex { get; set; }

        [Required(ErrorMessage = "Income is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Income cannot be negative.")]
        public decimal Income { get; set; }
    }
}
