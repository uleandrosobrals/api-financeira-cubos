using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Accounts
    {
        public Guid Id { get; set; }
        public string Branch { get; set; }
        public string Account { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Balance { get; set; }
        public List<Card> Cards { get; set; }

        [ForeignKey("People")]
        [Column(Order = 1)]
        public Guid PeopleId { get; set; }
        public virtual People People { get; set; }

    }
}
