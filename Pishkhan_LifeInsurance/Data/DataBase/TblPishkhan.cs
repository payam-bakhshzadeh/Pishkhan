using Pishkhan_LifeInsurance.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Data.DataBase
{
    public class TblPishkhan : TblBase
    {
        [Key]
        public int ID { get; set; }

        [Required (ErrorMessage =ValidationMessages.RequiredMessage)]
        [Display(Name ="نام دفتر پیشخوان")]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "کد دفتر پیشخوان")]
        public int Pishkhan_Code { get; set; }

        //Relations
        public virtual ICollection<TblLifeInsurance> TblLifeInsurance { get; set; }
    }
}
