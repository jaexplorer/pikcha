using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class ReturnDataModel
    {
        public string Status { get; set; }
        public int Statuscode { get; set; }
        public object Data { get; set; }
        public ReturnDataModel()
        {
            this.Status = "success";
            this.Statuscode = 200;
        }
        public ReturnDataModel(string status, int statusCode)
        {
            this.Status = status;
            this.Statuscode = statusCode;
        }
    }

    public enum STATUS_CODES
    {
        Success = 200,
        ExceptionThrown = 1901,
        UnsuccesfulUpdate = 1902

    }
}
