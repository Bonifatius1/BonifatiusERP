using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationPlanner.EntityModels.Models.Common
{
    public class KendoDataSourceRequestModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IList<KendoSortDescriptorModel> Sorts { get; set; }
        public IList<KendoFilterDescriptorModel> Filters { get; set; }
        public IList<AggregateDescriptorModel> Aggregates { get; set; }

        public IList<GroupDescriptor> Groups { get; set; }

        public string Module { get; set; }
        public int LanguageCode { get; set; }
    }

    public class FilterModel
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public FilterOperatorEnum FieldOperator { get; set; }
    }

    public class KendoFilterDescriptorModel
    {
        public FilterCompositionLogicalOperatorEnum? LogicalOperator { get; set; }
        public List<FilterModel> FilterList { get; set; }

        public FilterModel Filter { get; set; }
    }

    public class KendoSortDescriptorModel
    {
        public string FieldName { get; set; }
        public SortDirectionEnum SortDirection { get; set; }
    }

    public enum SortDirectionEnum
    {
        Ascending = 0,
        Descending = 1
    }

    public class GroupDescriptorModel
    {
        public string FieldName { get; set; }
    }

    public class AggregateDescriptorModel
    {

    }

    public enum FilterOperatorEnum
    {
        IsLessThan = 0,
        IsLessThanOrEqualTo = 1,
        IsEqualTo = 2,
        IsNotEqualTo = 3,
        IsGreaterThanOrEqualTo = 4,
        IsGreaterThan = 5,
        StartsWith = 6,
        EndsWith = 7,
        Contains = 8,
        IsContainedIn = 9,
        DoesNotContain = 10,
        IsNull = 11,
        IsNotNull = 12,
        IsEmpty = 13,
        IsNotEmpty = 14,
        IsNullOrEmpty = 15,
        IsNotNullOrEmpty = 16
    }

    public enum FilterCompositionLogicalOperatorEnum
    {
        And = 0,
        Or = 1
    }

    public class GridBinder
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int RecordCount { get; set; }

        public SortInfo SortInfo { get; set; } = new SortInfo() { Direction = SortDirection.Asc, Member = string.Empty };

        private readonly KendoDataSourceRequestModel _command;

        public GridBinder(KendoDataSourceRequestModel command)
        {
            _command = command;
            PageNumber = command.Page;
            PageSize = command.PageSize;
            if (command.Sorts != null)
                GetSortDescriptor();
        }

        private void GetSortDescriptor()
        {
            foreach (KendoSortDescriptorModel descriptor in _command.Sorts)
            {
                SortInfo.Member = descriptor.FieldName;
                SortInfo.Direction = descriptor.SortDirection == SortDirectionEnum.Ascending ? SortDirection.Asc : SortDirection.Desc;
            }
        }

        public string GetFilterDescriptor()
        {
            string filters = string.Empty;
            foreach (KendoFilterDescriptorModel filter in _command.Filters)
            {
                filters += ApplyFilter(filter);
                filters += " AND ";
            }

            filters = filters.Substring(0, filters.Length - 5);
            return filters;
        }

        private static string ApplyFilter(KendoFilterDescriptorModel filter)
        {
            var filters = "";
            if (filter.FilterList != null && filter.FilterList.Count > 0)
            {
                filters += "(";
                foreach (FilterModel filterchild in filter.FilterList)
                {
                    KendoFilterDescriptorModel childFilter = new KendoFilterDescriptorModel();

                    childFilter.Filter = filterchild;

                    filters += ApplyFilter(childFilter);
                    filters += " " + filter.LogicalOperator.ToString() + " ";
                }
            }
            else
            {
                string filterDescriptor = "{0} {1} {2}";
                var descriptor = filter.Filter;

                if (descriptor.FieldOperator == FilterOperatorEnum.StartsWith)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "LIKE", "'" + descriptor.FieldValue + "%'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.EndsWith)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "LIKE", "'%" + descriptor.FieldValue + "'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.Contains)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "LIKE", "'%" + descriptor.FieldValue + "%'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.DoesNotContain)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "NOT LIKE", "'%" + descriptor.FieldValue + "%'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.IsEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "=", "'" + descriptor.FieldValue + "'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.IsNotEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "<>", "'" + descriptor.FieldValue + "'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.IsGreaterThan)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, ">", "'" + descriptor.FieldValue + "'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.IsGreaterThanOrEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, ">=", "'" + descriptor.FieldValue + "'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.IsLessThan)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "<", "'" + descriptor.FieldValue + "'");
                }
                else if (descriptor.FieldOperator == FilterOperatorEnum.IsLessThanOrEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.FieldName, "<=", "'" + descriptor.FieldValue + "'");
                }

                filters = filterDescriptor;
            }

            filters = filters.EndsWith("And ") == true ? filters.Substring(0, filters.Length - 4) + ")" : filters;
            filters = filters.EndsWith("Or ") == true ? filters.Substring(0, filters.Length - 4) + ")" : filters;

            return filters;
        }
    }
    public class SortInfo
    {
        public string Member { get; set; }
        public SortDirection Direction { get; set; }
    }

    public enum SortDirection
    {
        Asc, Desc
    }
}
