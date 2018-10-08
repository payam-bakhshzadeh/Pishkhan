using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Models.YearAndMonth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ReportViewModels
{
    public class PishkhanReportGetViewModel
    {
        public PishkhanReportGetViewModel()
        {
            this.Pishkhan = new List<smallPishkhan>();
            this.YearAndMonth = new YearAndMonthViewModel();
            this.Setting = new TblSetting();
        }

        [Display(Name ="دفتر پیشخوان")]
        public List<smallPishkhan> Pishkhan { get; set; }

        public YearAndMonthViewModel YearAndMonth { get; set; }

        public TblSetting Setting { get; set; }
    }

    public class smallPishkhan
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
