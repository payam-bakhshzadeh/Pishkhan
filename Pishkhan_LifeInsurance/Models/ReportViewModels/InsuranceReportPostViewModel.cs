using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ReportViewModels
{
    public class InsuranceReportPostViewModel
    {
        public InsuranceReportPostViewModel()
        {
            this.DoublePrice = new List<ReportTableViewModel>();
            this.FirstTimePrice = new List<ReportTableViewModel>();
            this.NoPrice = new List<ReportTableViewModel>();
        }

        public List<ReportTableViewModel> FirstTimePrice { get; set; }
        public List<ReportTableViewModel> DoublePrice { get; set; }
        public List<ReportTableViewModel> NoPrice { get; set; }
    }
}
