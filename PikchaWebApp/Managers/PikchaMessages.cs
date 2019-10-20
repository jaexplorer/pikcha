using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Managers
{
    public class PikchaMessages
    {
        public const string MESS_Status416RequestedRangeNotSatisfiable = "You have reached the end.";
        public const string MESS_Status404NotFound = "Requested resource is not found.";

        public const string MESS_Status404_UserNotFound = "User not found.";
        public const string MESS_Status404_UserAlreadyPromoted = "User is already promoted.";
        public const string MESS_Status404_ProductNotFound = "Product not found.";
    }
}
