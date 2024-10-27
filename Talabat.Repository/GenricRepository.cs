using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenricRepository<T> : IGenricRepository<T> where T : BaseEntity
    {
        private readonly StroreContext _dbcontext;

        public GenricRepository(StroreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task Add(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
           _dbcontext.Set<T>().Update(entity);    
        }
        public void Delete(T entity)
        {
           _dbcontext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {

            return await _dbcontext.Set<T>().ToListAsync();

        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

       

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(),spec);
        }
    }
}
