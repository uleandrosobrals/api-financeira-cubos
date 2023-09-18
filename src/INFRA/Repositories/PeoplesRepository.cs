using DOMAIN.Entities;
using DOMAIN.Exceptions;
using DOMAIN.Interfaces.Repositories;
using INFRA.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRA.Repositories
{
    public class PeoplesRepository : IPeopleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PeoplesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        async Task<People> IPeopleRepository.CreateAsync(People People)
        {
            var existingPeople = await _dbContext.People.FirstOrDefaultAsync(p => p.Document == People.Document);

            if (existingPeople != null)
            {
                throw new BusinessException("Documento já existe.");
            }

            _dbContext.People.Add(People);
            await _dbContext.SaveChangesAsync();

            return People;
        }

        
    }
}
