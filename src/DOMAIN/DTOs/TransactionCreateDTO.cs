using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DTOs
{
    public class TransactionCreateDTO
    {
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}

