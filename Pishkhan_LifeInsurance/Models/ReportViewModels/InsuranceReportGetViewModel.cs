using Pishkhan_LifeInsurance.Models.YearAndMonth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ReportViewModels
{
    public class InsuranceReportGetViewModel
    {

        public InsuranceReportGetViewModel()
        {
            this.YearAndMonth = new YearAndMonthViewModel();
        }
        public  YearAndMonthViewModel YearAndMonth { get; set; }
    }
}
