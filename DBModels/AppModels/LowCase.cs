using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class LowCase:MainModel
    {
        [Display(Name = "Case"), Required(ErrorMessage = "required")]
        public string Case { get; set; }
        [Display(Name = "CaseDoc"), Required(ErrorMessage = "required")]
        public string CaseDoc { get; set; }
        [Display(Name = "StartDate"), Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "EndDate")]
        public DateTime? EndDate { get; set; }

        [ForeignKey("Claimant")]
        [Display(Name = "Claimant"), Required(ErrorMessage = "required")]
        public long ClaimantId { get; set; }
        [Display(Name = "Claimant")]
        public Claimant? Claimant { get; set; }

        [ForeignKey("Lowyer")]
        [Display(Name = "Lowyer"), Required(ErrorMessage = "required")]
        public long LoweyrId { get; set; }
        [Display(Name = "Lowyer")]
        public Lowyer? Lowyer { get; set; }
    }
}
