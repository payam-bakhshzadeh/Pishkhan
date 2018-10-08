using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Const;

namespace Pishkhan_LifeInsurance.Models
{
    // Add profile data for application users by adding properties to the TblUser class
    public class TblUser : IdentityUser
    {
        [Required(ErrorMessage =ValidationMessages.RequiredMessage)]
        [Display(Name ="کد نمایندگی")]
        public int AgentID { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "نام و نام خانوادگی")]
        public string Name { get; set; }


        //Relations
        public virtual ICollection<TblLifeInsurance> TblLifeInsurance { get; set; }
        public virtual ICollection<TblKarmozd> TblKarmozd { get; set; }
    }
}
