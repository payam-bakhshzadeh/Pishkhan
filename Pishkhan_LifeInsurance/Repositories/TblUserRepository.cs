using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pishkhan_LifeInsurance.Models;
using Pishkhan_LifeInsurance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pishkhan_LifeInsurance.Models.AgentViewModels;

namespace Pishkhan_LifeInsurance.Repositories
{
     public class TblUserRepository : ITblUserRepository
    {
        private readonly ApplicationDbContext db;

        public TblUserRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<List<TblUser>> GetAgentListAsync()
        {
            try
            {
                var model = new AgentListViewModels();
                return await db.Users.ToListAsync();
            }
            catch 
            {

                var list = new List<TblUser>();
                return list;
            }
            
        }

        public bool UpdateUserInfo(TblUser user)
        {
            try
            {
                db.Update(user);
                db.SaveChanges();
                return true;
            }
            catch 
            {

                return false;
            }
        }
    }
}
