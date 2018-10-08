using Pishkhan_LifeInsurance.Models.InsuranceListViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ManageViewModels
{
    public class ListInsuranceViewModel
    {
        public ListInsuranceViewModel()
        {
            this.AgentListVM = new List<AgentListViewModel>();
            this.PishkhanListVM = new List<PishkhanListViewModel>();
            this.SupervisiorListVM = new List<SupervisiorListViewModel>();
            this.TabelItemsVM = new List<TabelItemsViewModel>();

        }

        public FilterItemsViewModel FilterItemsVM { get; set; }
        public List<AgentListViewModel> AgentListVM { get; set; }
        public List<PishkhanListViewModel> PishkhanListVM { get; set; }
        public List<SupervisiorListViewModel> SupervisiorListVM { get; set; }
        public List<TabelItemsViewModel> TabelItemsVM { get; set; }

        public int CountTotalRow { get; set; }
        public int CountRow { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
