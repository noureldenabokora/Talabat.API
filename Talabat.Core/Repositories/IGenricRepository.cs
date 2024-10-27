﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specification;

namespace Talabat.Core.Repositories
{
    public interface IGenricRepository<T> where T : BaseEntity
    {

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);


        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);

        Task Add (T entity);

        void Update (T entity);

        void Delete (T entity);
    }
}
