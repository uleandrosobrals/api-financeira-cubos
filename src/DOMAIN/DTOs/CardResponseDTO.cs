using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DTOs
{
    public class CardResponseDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
