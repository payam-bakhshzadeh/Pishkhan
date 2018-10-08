using Pishkhan_LifeInsurance.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Services
{
    public interface IRepository<T> where T : TblBase
    {
        Task<List<T>> GetAll();
        Task<T> GetByID(int id);
        Task<bool> Add(T obj);
        Task<bool> AddRange(List<T> obj);
        bool Update(T obj);
        bool Delete(T obj);
        bool DeleteRange(List<T> obj);
    }
}
