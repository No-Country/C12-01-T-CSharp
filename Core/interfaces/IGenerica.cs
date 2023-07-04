using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.interfaces
{
    public interface IGenerica<T> where T : BaseEntidad
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(IEspecificaciones<T> spec);
        Task<IReadOnlyList<T>> ListAsync(IEspecificaciones<T> spec);
        Task<int> CountAsync(IEspecificaciones<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
