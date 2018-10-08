using Pishkhan_LifeInsurance.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.LifeInsuranceViewModels
{
    public class LifeInsuranceViewModels
    {
        public int ID { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "نماینده")]
        public string UserID { get; set; }

        [Display(Name = "دفتر پیشخوان")]
        public int? PishkhanID { get; set; }

        [Display(Name = "زیرگروه")]
        public int? SupervisorID { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "شماره سریال")]
        public int Serial { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "کد")]
        public int InsuranceNumber_Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "واحد سرپرستی")]
        public int InsuranceNumber_CenterCode { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "شماره قرارداد")]
        public string InsuranceNumber_GarardadNumber { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "سال")]
        public int InsuranceNumber_Year { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "شماره ")]
        public int InsuranceNumber_Number { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "نام و نام خانوادگی بیمه گذار ")]
        public string Bimegozar_Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "نام و نام خانوادگی بیمه شده ")]
        public string Bimeshode_Name { get; set; }

        [Display(Name = "موبایل بیمه گذار ")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = ValidationMessages.MobileLength)]
        public string Bimegozar_Phone { get; set; }

        [Display(Name = "موبایل بیمه شده ")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = ValidationMessages.MobileLength)]
        public string Bimeshode_Phone { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "تاریخ صدور بیمه نامه ")]
        public string Date_Soodoor_Shamsi { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "تاریخ میلادی صدور بیمه نامه ")]
        public DateTime Date_Soodoor_Miladi { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "تاریخ شروع بیمه نامه ")]
        public string Date_Start_Shamsi { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "تاریخ میلادی شروع بیمه نامه ")]
        public DateTime Date_Start_Miladi { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "نحوه پرداخت ")]
        public int PaymentType { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "حق بیمه ماهانه ")]
        public int Payment_Price { get; set; }


        [Display(Name = "سپرده اولیه ")]
        public int? SepordeAvaliye { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "وضعیت بیمه نامه ")]
        public bool Insurance_Status { get; set; }

        [Display(Name = "خارج از حوزه صادر گردیده ")]
        public bool OutOfOffice { get; set; }
    }
}
