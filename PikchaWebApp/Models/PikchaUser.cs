using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    [Table("PikchaUsers")]
    public class PikchaUser : IdentityUser
    {
        // personal info
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string AvatarFileName { get; set; }
        public string SignatureFileName { get; set; }
        public string BioInfo { get; set; }
        

        // permenant address
        public string Address_1 { get; set; }

        public string Address_2 { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        // social media links
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string LinkedInLink { get; set; }

    }
}
