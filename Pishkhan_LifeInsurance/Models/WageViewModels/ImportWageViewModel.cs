using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.WageViewModels
{
    public class ImportWageViewModel
    {
        public ImportWageViewModel()
        {
            this.Users = new List<WageViewModels.Users>();
            this.Year = new List<int>();
            this.Month = new List<int>();
        }

        [Display(Name ="نماینده ")]
        [Required(ErrorMessage =(Const.ValidationMessages.RequiredMessage))]
        public List<Users> Users { get; set; }

        [Display(Name = "سال کامزد ")]
        [Required(ErrorMessage = (Const.ValidationMessages.RequiredMessage))]
        public List<int> Year { get; set; }

        [Display(Name = "ماه کارمزد ")]
        [Required(ErrorMessage = (Const.ValidationMessages.RequiredMessage))]
        public List<int> Month { get; set; }

        public List<string> ExcelFileList { get; set; }
    }

    public class Users
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }


}
