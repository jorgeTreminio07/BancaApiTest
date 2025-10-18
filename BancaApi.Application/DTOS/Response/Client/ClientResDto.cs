using BancaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Response.Client
{
    public class ClientResDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public SexType Sex { get; set; }
        public decimal? Income { get; set; }
    }
}
