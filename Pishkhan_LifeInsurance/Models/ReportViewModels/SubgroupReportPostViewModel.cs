using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ReportViewModels
{
    public class SubgroupReportPostViewModel
    {
        public SubgroupReportPostViewModel()
        {
            this.DoubleWage = new List<ReportTableViewModel>();
            this.FirstWage = new List<ReportTableViewModel>();
            this.NoPriceWage = new List<ReportTableViewModel>();
        }

        public List<ReportTableViewModel> FirstWage { get; set; }
        public List<ReportTableViewModel> DoubleWage { get; set; }
        public List<ReportTableViewModel> NoPriceWage { get; set; }

        public int FirstWagePercent { get; set; }
        public int DoubleWagePercent { get; set; }
    }
}
