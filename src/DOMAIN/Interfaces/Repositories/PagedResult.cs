using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Repositories
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } // Lista de itens na página atual
        public int TotalItems { get; set; } // Número total de itens na consulta
        public int PageNumber { get; set; } // Número da página atual
        public int PageSize { get; set; } // Tamanho da página (número de itens por página)
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize); // Número total de páginas

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
