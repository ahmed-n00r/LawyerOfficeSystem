using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class ContacteType:MainModel
    {
        [Display(Name = "name"), Required(ErrorMessage = "required")]
        public string Name { get; set; }
    }
}
