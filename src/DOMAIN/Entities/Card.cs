using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("Accounts")]
        [Column(Order = 1)]
        public Guid AccountsId { get; set; }
        public virtual Accounts Accounts { get; set; }


        [NotMapped]
        public string LastFourDigits => Number != null && Number.Length >= 4 ? Number.Substring(Number.Length - 4) : string.Empty;
    }
}

