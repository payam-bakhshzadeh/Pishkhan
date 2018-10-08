using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.WageViewModels
{
    public class DeleteWageViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string AgentName { get; set; }
        public string AgentID { get; set; }
    }
}
