using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Models.AgentViewModels;
using Pishkhan_LifeInsurance.Models.LifeInsuranceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Views.LifeInsurance
{
    public class AddViewModels
    {
        public AddViewModels()
        {
            this.LifeInsuranceVM = new LifeInsuranceViewModels();
            this.AgentVM = new List<AgentListViewModels>();
            this.PishkhanVm = new List<TblPishkhan>();
            this.SupervisiorVm = new List<TblSupervisior>();
        }

        public LifeInsuranceViewModels LifeInsuranceVM { get; set; }
        public List<AgentListViewModels> AgentVM { get; set; }
        public List<TblPishkhan> PishkhanVm { get; set; }
        public List<TblSupervisior> SupervisiorVm { get; set; }
        public List<string> PaymentType { get; set; }
        public List<string> InsuranceStatus { get; set; }




    }
}
