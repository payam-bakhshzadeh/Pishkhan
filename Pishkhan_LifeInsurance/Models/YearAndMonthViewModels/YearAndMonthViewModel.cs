using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.YearAndMonth
{
    public class YearAndMonthViewModel
    {

        public YearAndMonthViewModel()
        {
            this.ddlMonth = new List<int>();
            this.ddlYear = new List<int>();

            for (int i = 1396; i < 1420; i++)
            {
                ddlYear.Add(i);
            }

            for (int i = 1; i < 13; i++)
            {
                ddlMonth.Add(i);
            }

        }

        [Display(Name ="سال")]
        public List<int> ddlYear { get; set; }

        [Display(Name = "ماه")]
        public List<int> ddlMonth { get; set; }
    }
}
