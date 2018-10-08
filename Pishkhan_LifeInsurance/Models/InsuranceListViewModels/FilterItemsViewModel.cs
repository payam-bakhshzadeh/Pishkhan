using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.InsuranceListViewModels
{
    public class FilterItemsViewModel
    {
        public string AgentID { get; set; }
        public int? PishkhanID { get; set; }
        public int? SupervisiorID { get; set; }
        public int? InsuranceNumber { get; set; }
        public int? InsuranceSerial { get; set; }
        public string BimegozarName { get; set; }
        public string BimeshodeName { get; set; }
        public string BimegozarPhone { get; set; }
        public string BimeshodePhone { get; set; }
        public string FromDate_Soodoor { get; set; }
        public string ToDate_Soodoor { get; set; }
        public string FromDate_Start { get; set; }
        public string ToDate_Start { get; set; }
    }
}
