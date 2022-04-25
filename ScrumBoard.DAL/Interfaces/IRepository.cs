using System;
using System.Collections.Generic;

namespace ScrumBoardLibrary.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int? id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(string name);
        void Update(T item);
        void Remove(int? id);
    }
}