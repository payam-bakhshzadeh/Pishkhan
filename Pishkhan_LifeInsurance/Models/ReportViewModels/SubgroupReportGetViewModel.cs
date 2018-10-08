using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Models.YearAndMonth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ReportViewModels
{
    public class SubgroupReportGetViewModel
    {

        public SubgroupReportGetViewModel()
        {
            this.Setting = new TblSetting();
            this.Subgroup = new List<smallSubgroup>();
            this.YearAndMonth = new YearAndMonthViewModel();
        }

        [Display(Name ="زیرگروه")]
        public List<smallSubgroup> Subgroup { get; set; }

        public YearAndMonthViewModel YearAndMonth { get; set; }

        public TblSetting Setting { get; set; }
    }

    public class smallSubgroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
