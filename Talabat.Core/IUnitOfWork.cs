using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories;

namespace Talabat.Core
{
    public interface IUnitOfWork: IAsyncDisposable 
    {
        IGenricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


        // take int to return number of changes 
        Task<int> Complete();

    }
}
