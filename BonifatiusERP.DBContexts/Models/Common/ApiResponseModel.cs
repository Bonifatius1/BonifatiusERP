using System;
using System.Collections.Generic;
using System.Text;
using static BonifatiusERP.Models.Enum.Enum;

namespace BonifatiusERP.Models.Models.Common
{
    public class ApiResponseModel
    {
        public ApiResponseModel()
        {
            Code = (int)ResponseStatusCode.OK;
        }
        public string Message { get; set; }

        public int Code { get; set; }

        public dynamic Data { get; set; }
        
    }

    public class LoginResponseModel
    {
        public string Message { get; set; }

        public int Code { get; set; }

        public string Token { get; set; }

        public UserModel Data { get; set; }

    }

    public class UserModel
    {
        public Guid Id { get; set; }

        public Guid UserProfileId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePhoto { get; set; }

        public string PhoneNumber { get; set; }
        public string UserCurrency { get; set; }
        public double UserCurrencyAmount { get; set; }

        public string WeatherType { get; set; }

        public string DistanceType { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public  int? RegisterType { get; set; }
        public  int? ClientId { get; set; }
        public  bool? IsAdminUser { get; set; }
        public  List<int> ActionPermissions { get; set; }

    }
    public class EmailResponseModel
    {
        public Guid? Id { get; set; }
        public string Subject { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Ccemail { get; set; }
        public string Body { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? Createddate { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool? IsSuccess { get; set; }
    }

}
