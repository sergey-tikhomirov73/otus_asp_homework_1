using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>
        : IRepository<T>
        where T: BaseEntity
    {
        //  protected IEnumerable<T> Data { get; set; }
        protected List<T> Data { get; set; }

        public InMemoryRepository(List<T> data)
        {
            Data = data;
        }
        
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data.AsEnumerable<T>());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        Task<T> IRepository<T>.CreateAsync(T item)
        {
            item.Id = Guid.NewGuid(); // создаем новый Id для item

            Data.Add(item);// добавляем в коллекцию
                    
            return Task.FromResult(item);
            
        }

        Task<T> IRepository<T>.UpdateAsync(T newItem)
        {
            T oldItem;
            oldItem=Data.FirstOrDefault( x => x.Id == newItem.Id ); // ищем элемент в коллекции

            if ( oldItem == null) throw new Exception($"При обновлении в репозитории не найдена сущность с ID {newItem.Id}");
            Data.Remove(oldItem);
            Data.Add(newItem);
           return Task.FromResult(newItem);
        }

        Task<T> IRepository<T>.DeleteAsync(Guid id)
        {
           T item = Data.FirstOrDefault(x => x.Id == id); // ищем элемент в коллекции по id

            if ( item == null ) throw new Exception($"При удалении из репозитория не найдена сущность с ID {id}");

            Data.Remove(item);
         
            return Task.FromResult(item);
        }
    }
}