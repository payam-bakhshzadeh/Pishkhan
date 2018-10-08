using Pishkhan_LifeInsurance.Models.YearAndMonth;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ReportViewModels
{
    public class AgentReportGetViewModel
    {

        public AgentReportGetViewModel()
        {
            this.Agent = new List<smallAgent>();
            this.YearAndMonth = new YearAndMonthViewModel();
        }

        public List<smallAgent> Agent { get; set; }
        public YearAndMonthViewModel YearAndMonth { get; set; }

        public int From_InsuranceNumber { get; set; }
        public int To_InsuranceNumber { get; set; }
    }

    public class smallAgent
    {
        public string ID { get; set; }

        [Display(Name ="نماینده")]
        public string Name { get; set; }
    }
}
