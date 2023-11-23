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
        [Display(Name = "identity"), Required(ErrorMessage = "required")]
        public string Identity { get; set; }

        [ForeignKey("IdentityType")]
        [Display(Name = "identityType"), Required(ErrorMessage = "required")]
        public long IdentityTypeId { get; set; }

        [Display(Name = "identityType")]
        public IdentityType IdentityType { get; set; }
    }
}
