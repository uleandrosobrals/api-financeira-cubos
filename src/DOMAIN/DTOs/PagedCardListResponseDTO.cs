using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DTOs
{
    public class PagedCardListResponseDTO
    {
        public List<CardResponseDTO> Cards { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
