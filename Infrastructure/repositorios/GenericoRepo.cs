using Core.Entities;
using Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.repositorios
{
    public class GenericoRepo<T> : IGenerica<T> where T : BaseEntidad
    {
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(IEspecificaciones<T> spec)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntityWithSpec(IEspecificaciones<T> spec)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> ListAsync(IEspecificaciones<T> spec)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
