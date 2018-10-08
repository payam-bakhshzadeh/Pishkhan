using Pishkhan_LifeInsurance.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Models.AgentViewModels
{
    public class AgentListViewModels
    {
        [Required]
        public string ID { get; set; }

        [Required(ErrorMessage =ValidationMessages.RequiredMessage)]
        public int AgentID { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Email { get; set; }

        public static implicit operator List<object>(AgentListViewModels v)
        {
            throw new NotImplementedException();
        }
    }
}
