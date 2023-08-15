using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationPlanner.EntityModels.Models.Travel;

namespace VacationPlanner.EntityModels.Models.Common
{
    public class SignatureDocumentGridModel
    {
        public dynamic Data { get; set; }
        public TravelPlanSharedViewModel SharedData { get; set; }

        public int Total { get; set; }

        public string Currency { get; set; }
    }
}
