using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationPlanner.EntityModels.Models.Common
{
    public class EmailNotificationModel
    {
        public bool IsRecurring { get; set; }
        public int FrequencyNumber { get; set; }
        public string FrequencyType { get; set; }
        public int? TillFrequencyNumber { get; set; }
        public string TillFrequencyType { get; set; }
        public int? AfterFrequencyNumber { get; set; }
        public string AfterFrequencyType { get; set; }
    }

    public class EmailMatchDateModel
    {
        public List<DateTime> MatchDateList { get; set; }
        public List<EmailNotificationModel> EmailNotificationModelList { get; set; }
    }

    public class EmailTemplateModel
    {
        public int EmailTemplateId { get; set; }
        public string EmailTemplateName { get; set; }
        public int SchedulerFrequencyNumber { get; set; }
        public string SchedulerFrequencyType { get; set; }
    }

    public class InCompleteBookingTravelDataModel
    {
        public Guid TravelPlanId { get; set; }
        public int TripLocationCount { get; set; }
        public int HotelCount { get; set; }
        public int FlightCount { get; set; }
        public int TripLocationAirTravelCount { get; set; }
    }

    public class LocationDetailDataModel
    {
        public Guid TravelPlanId { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public int Sequence { get; set; }
    }
    public class LocationActivityDataModel
    {
        public string CityStateCountryName { get; set; }
        public List<LocationActivityDetailDataModel> ActivityData { get; set; }
    }

    public class LocationActivityDetailDataModel
    {
        public string PhotoReference { get; set; }
        public string Address { get; set; }
        public string ActivityName { get; set; }
    }
}