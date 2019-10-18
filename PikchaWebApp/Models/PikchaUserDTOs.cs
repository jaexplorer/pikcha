using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaUserDTOs
    {
    }

    public class Pikcha100ArtistDTO : PikchaUserBaseDTO
    {
        public string PerCountry { get; set; }
        public string BioInfo { get; set; }
        public string SocialLinks { get; set; }
        
        public long TotalImageViews { get; set; }

        public decimal Performance { get; set; }

        public int TotalImageSold { get; set; }
        public decimal AveragePrice { get; set; }

        public string TopImageTitle { get; set; }
        public string TopImageLocation { get; set; }
        public string TopImageThumbnailFile { get; set; }
        public string TopImageWatermarkedFile { get; set; }
        public long TopImageTotalViews { get; set; }



        //public PikchaUser Artist { get; set; }
        //public IEnumerable<PikchaImageTag> ImageTags { get; set; }

        // public List<PikchaImageViews> PikchaImageViews { get; set; }

        //public int PikchaImageViewsCount { get; set; }

    }


    public class PikchaAuthenticatedUserDTO : PikchaUserBaseDTO
    {
        public string BioInfo { get; set; }
        public string SocialLinks { get; set; }

        public string ShipAddress1 { get; set; }

        public string ShipAddress2 { get; set; }

        public string ShipCity { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        public List<PikchaUserBaseDTO> Following { get; set; }

        public DateTimeOffset LastUploadedOn { get; set; }

        public string Roles { get; set; }

    }


    public class PikchaUserBaseDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarFileName { get; set; }

    }

}
