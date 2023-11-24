using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class Claimant:MainModel
    {
        [Display(Name = "name"), Required(ErrorMessage = "required")]
        public string Name { get; set; }
        [Display(Name = "Identity"), Required(ErrorMessage = "required")]
        public string Identity { get; set; }

        [ForeignKey("IdentityType")]
        [Display(Name = "IdentityType"), Required(ErrorMessage = "required")]
        public long IdentityTypeId { get; set; }

        [Display(Name = "IdentityType")]
        public IdentityType? IdentityType { get; set; }
    }
}
