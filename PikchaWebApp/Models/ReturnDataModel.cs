using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class ReturnDataModel
    {
        public string Status { get; set; }
        public string Statuscode { get; set; }
        public object Data { get; set; }
        public ReturnDataModel()
        {
            this.Status = "success";
            this.Statuscode = "200";
        }
        public ReturnDataModel(string status, string statusCode)
        {
            this.Status = status;
            this.Statuscode = statusCode;
        }
    }

    public enum STATUS_CODES
    {
        Success = 200,
        ExceptionThrown = 1901

    }
}
