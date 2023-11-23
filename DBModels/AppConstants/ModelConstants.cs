using DBModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppConstants
{
    public class ModelConstants
    {
        public static ModelData HomeModel { get => new() { IsActive = ModelActivationStatus.Active, Name = "Home", MainAction = "Index", Icon = "ni ni-tv-2 text-primary", Index = 1, VeiwName = "Dashboard" }; }

        public static ModelData TestingModel { get => new() { IsActive = ModelActivationStatus.NotActive, Name = "ContacteTypes", MainAction = "Index", Icon = "ni ni-send text-primary", Index = 2, VeiwName = "ContacteTypes" }; }
        //
        public static List<ModelData> ModelList = new List<ModelData> {
                HomeModel,TestingModel
            }.OrderBy(k => k.Index).ToList();

        public static void setAllNotActive(string controllerName)
        {
            foreach(var item in ModelList)
            {
                item.IsActive = item.Name.Equals(controllerName, StringComparison.OrdinalIgnoreCase)
                    ?  ModelActivationStatus.Active
                    : ModelActivationStatus.NotActive;
            }
        }
    }
}
