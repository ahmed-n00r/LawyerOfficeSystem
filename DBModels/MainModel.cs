using DBModels.AppConstants;
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
        public string? status { get; set; }
        [Display(Name = "EnterdDate")]
        public DateTime? EnterdDate { get; set; }
        [Display(Name = "UpdateDate")]
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "DeleteDate")]
        public DateTime? DeleteDate { get; set; }

        [Display(Name = "EnterBy")]
        public string? EnterBy { get; set; }
        [Display(Name = "UpdateBy")]
        public string? UpdateBy { get; set; }
        [Display(Name = "DeleteBy")]
        public string? DeleteBy { get; set; }

        public void addModel()
        {
            status = ModelActivationStatus.Active.ToString();
            EnterdDate = DateTime.Now;
            //EnterBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }
        public void updateModel()
        {
            status = ModelActivationStatus.Update.ToString(); ;
            UpdateDate = DateTime.Now;
            //EnterBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }
        public void deleteModel()
        {
            status = ModelActivationStatus.Delete.ToString(); ;
            DeleteDate = DateTime.Now;
            //EnterBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }
    }
}
