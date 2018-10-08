using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.ReportViewModels
{
    public class ReportTableViewModel
    {
        public int Serial { get; set; }
        public string InsuranceNumber { get; set; }
        public string BimegozarName { get; set; }
        public string BimeshodeName { get; set; }
        public string SoodoorDate { get; set; }
        public int Price { get; set; }
        public int PaymentType { get; set; }
        public bool Status { get; set; }
        public bool OutOfOffice { get; set; }
        public int Wage { get; set; }

    }
}
