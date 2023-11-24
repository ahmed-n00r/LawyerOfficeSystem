using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class Lowyer:MainModel
    {
        [Display(Name = "name"), Required(ErrorMessage = "required")]
        public string Name { get; set; }
        [Display(Name = "LowyerNo"), Required(ErrorMessage = "required")]
        public string LowyerNo { get; set; }
    }
}
