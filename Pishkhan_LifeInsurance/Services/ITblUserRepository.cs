using Pishkhan_LifeInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Services
{
    public interface ITblUserRepository
    {
        Task<List<TblUser>> GetAgentListAsync();

        bool UpdateUserInfo(TblUser user);
    }
}
