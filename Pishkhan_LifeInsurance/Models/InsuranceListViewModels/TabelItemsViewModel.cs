using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.InsuranceListViewModels
{
    public class TabelItemsViewModel
    {
        public int ID { get; set; }
        public string BimegozarName { get; set; }
        public string BimeshodeName { get; set; }
        public string BimegozarPhone { get; set; }
        public string BimeshodePhon { get; set; }
        public string DateSoodoor { get; set; }
        public string DateStart { get; set; }
        public string InsuranceNumber { get; set; }
        public bool Status { get; set; }
        public int PaymentType { get; set; }
        public int Price { get; set; }
        public string Pishkhan { get; set; }
        public string Supervisior { get; set; }
        public string Agent { get; set; }

        public string TarikhSabtBimename { get; set; }
    }
}
