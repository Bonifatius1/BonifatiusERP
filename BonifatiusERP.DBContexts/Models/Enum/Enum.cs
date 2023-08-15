using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace BonifatiusERP.Models.Enum
{
    public class Enum
    {
       
        public enum ResponseStatusCode : int
        {
            OK = 200,
            BadRequest = 400,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            Conflict = 409,
            InternalServerError = 500,
            NotActivated = 300,
            UpgradeRequired = 426
        }

        
    }
}
