using Pishkhan_LifeInsurance.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Data.DataBase
{
    public class TblSetting : TblBase
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "کد شماره بیمه نامه")]
        public int InsuranceNumber_Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "واحد سرپرستی شماره بیمه نامه")]
        public int InsuranceNumber_CenterCode { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "شماره قرارداد شماره بیمه نامه")]
        public string InsuranceNumber_GarardadNumber { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "سال شماره بیمه نامه")]
        public int InsuranceNumber_Year { get; set; }


        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "درصد کارمزد بیمه نامه های جدید دفاتر پیشخوان")]
        public int Pishkhan_Percent { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "درصد کارمزد بیمه نامه های جدید زیرگروه ها")]
        public int Supervisior_Percent { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "درصد کارمزد بیمه نامه های قدیم دفاتر پیشخوان")]
        public int Pishkhan_Percent_OldInsurance { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "درصد کارمزد بیمه نامه های قدیم زیرگروه ها")]
        public int Supervisior_Percent_OldInsurance { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "درصد کارمزد بیمه نامه های صادره خارج از دفتر پیشخوان")]
        public int Supervisior_Percent_OutOfOffice { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "تعداد سطرهای قابل نمایس در هر جدول")]
        public int CountTakeItem { get; set; }
    }
}
