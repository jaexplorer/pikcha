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
        public const string MESS_Status404_ArtistNotFound = "Artist not found.";
        public const string MESS_Status404_UserAlreadyUnfollowed = "User is already unfollowed the artist.";
        public const string MESS_Status404_UserCantFollowHimself = "You can't follow yourself.";

        public const string MESS_Status404ImageNotFound = "Image not found.";
        public const string MESS_Status404SignatureNotFound = "Signature not found.";


        public const string MESS_Status500InternalServerError = "Internal server error occured. Please try again later.";
        public const string MESS_Status500ImageProcessingError = "Image processing occured. Please try again later.";



    }
}
