using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StroreContext _dbcontext;

       // private Dictionary<string, GenricRepository<BaseEntity>> _repositories;
        private Hashtable _repositories;

        public UnitOfWork(StroreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Hashtable();

            // _repositories = new Dictionary<string, GenricRepository<BaseEntity>>();
        }
        public IGenricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenricRepository<TEntity>(_dbcontext);

                _repositories.Add(type, repository);
            }

            return _repositories[type] as IGenricRepository<TEntity>;
        }

        public async Task<int> Complete()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _dbcontext.DisposeAsync(); 
        }

    }
}
