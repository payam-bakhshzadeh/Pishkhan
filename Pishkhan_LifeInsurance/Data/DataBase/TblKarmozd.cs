using Pishkhan_LifeInsurance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Data.DataBase
{
    public class TblKarmozd : TblBase
    {
        [Key]
        public int ID { get; set; }

        public string AgentID { get; set; }

        public int TblLifeInsuranceID { get; set; }

        [Required(ErrorMessage =Const.ValidationMessages.RequiredMessage)]
        [Display(Name = "سال")]
        public int Year { get; set; }

        [Required(ErrorMessage = Const.ValidationMessages.RequiredMessage)]
        [Display(Name = "ماه")]
        public int Month { get; set; }
        public int Price { get; set; } = 0;
        public bool isDouble { get; set; }


        [ForeignKey(nameof(TblLifeInsuranceID))]
        public virtual TblLifeInsurance TblLifeInsurance { get; set; }

        [ForeignKey(nameof(AgentID))]
        public virtual TblUser TblUser { get; set; }

    }
}
