using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DTOs
{
    public class TransactionListResponseDTO
    {
        public List<TransactionResponseDTO> Transactions { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}

