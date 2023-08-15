using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationPlanner.EntityModels.Models.Common
{
    public class OrganizationModel
    {
        public string SearchText { get; set; }
        public DataSourceRequestModel KendoRequest { get; set; }
    }
}
