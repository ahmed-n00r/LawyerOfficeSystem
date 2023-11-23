using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class MainModel
    {
        [Key]
        public long ID { get; set; }

        [Display(Name = "status")]
        public string status { get; set; }
        [Display(Name = "EnterdDate")]
        public DateTime EnterdDate { get; set; }
        [Display(Name = "UpdateDate")]
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "DeleteDate")]
        public DateTime? DeleteDate { get; set; }

        [Display(Name = "EnterBy")]
        public string EnterBy { get; set; }
        [Display(Name = "UpdateBy")]
        public string? UpdateBy { get; set; }
        [Display(Name = "DeleteBy")]
        public string? DeleteBy { get; set; }
    }
}
