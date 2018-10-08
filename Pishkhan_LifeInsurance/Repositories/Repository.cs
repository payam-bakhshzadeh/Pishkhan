using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pishkhan_LifeInsurance.Data;

namespace Pishkhan_LifeInsurance.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : TblBase
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }



        public async Task<bool> Add(T obj)
        {
            try
            {
                await _db.Set<T>().AddAsync(obj);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public async Task<bool> AddRange(List<T> obj)
        {
            try
            {
                await _db.Set<T>().AddRangeAsync(obj);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public bool Delete(T obj)
        {
            try
            {
                _db.Set<T>().Remove(obj);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public bool DeleteRange(List<T> obj)
        {
            try
            {
                _db.Set<T>().RemoveRange(obj);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _db.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<T> GetByID(int id)
        {
            try
            {
                return await _db.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Update(T obj)
        {
            try
            {
                _db.Set<T>().Update(obj);
                return true;
            }
            catch 
            {

                return false;
            }
        }
    }
}
