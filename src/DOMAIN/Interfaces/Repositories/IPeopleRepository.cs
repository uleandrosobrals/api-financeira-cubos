using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Repositories
{
    public interface IPeopleRepository
    {
        Task<People> CreateAsync(People People);
        
    }
}
