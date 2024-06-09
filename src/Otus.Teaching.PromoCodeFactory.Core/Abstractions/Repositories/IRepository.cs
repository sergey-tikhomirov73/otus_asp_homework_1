using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository
        <T>
        where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T item);// создать новый
        Task<T> UpdateAsync(T item);// обновить
        Task<T> DeleteAsync(Guid id);// удалить

    }
}