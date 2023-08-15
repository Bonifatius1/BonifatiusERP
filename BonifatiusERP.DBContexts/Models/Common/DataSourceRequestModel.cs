using System;
using System.Collections.Generic;
using System.Text;

namespace VacationPlanner.EntityModels.Models.Common
{
    public class DataSourceRequestModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public IList<SortDescriptorModel> Sorts { get; set; }
        public IList<FilterDescriptorModel> Filters { get; set; }
    }

    public class SortDescriptorModel
    {
        public string Field { get; set; }
        public int Direction { get; set; }
    }

    public class FilterDescriptorModel
    {
        public string Field { get; set; }
        public int OperatorType { get; set; }
        public object Value { get; set; }
    }
}
