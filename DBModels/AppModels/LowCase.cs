using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class LowCase:MainModel
    {
        public string Case { get; set; }
        public string CaseDoc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Claimant")]
        public long ClaimantId { get; set; }
        public Claimant Claimant { get; set; }

        [ForeignKey("Lowyer")]
        public long LoweyrId { get; set; }
        public Lowyer Lowyer { get; set; }
    }
}
