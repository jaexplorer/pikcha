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
        public string PikchaImageId { get; set; } = string.Empty;
        public string BioInfo { get; set; } = string.Empty;
        public Dictionary<string, string> SocialLinks { get; set; } = new Dictionary<string, string>();

        public long TotalImageViews { get; set; } = 0;

        public decimal Performance { get; set; } = 0;

        public int TotalImageSold { get; set; } = 0;
        public decimal AveragePrice { get; set; } = 0;

        public string TopImageTitle { get; set; } = string.Empty;
        public string TopImageLocation { get; set; } = string.Empty;
        public string TopImageThumbnailFile { get; set; } = string.Empty;
        public string TopImageWatermarkedFile { get; set; } = string.Empty;
        public long TopImageTotalViews { get; set; } = 0;
        public List<PikchaUserBaseDTO> Following { get; set; } = new List<PikchaUserBaseDTO>();



        //public PikchaUser Artist { get; set; }
        //public IEnumerable<PikchaImageTag> ImageTags { get; set; }

        // public List<PikchaImageViews> PikchaImageViews { get; set; }

        //public int PikchaImageViewsCount { get; set; }

    }


    public class PikchaAuthenticatedUserDTO : PikchaUserBaseDTO
    {
        public string BioInfo { get; set; } = string.Empty;
        public string SocialLinks { get; set; } = string.Empty;

        public string ShipAddress1 { get; set; } = string.Empty;

        public string ShipAddress2 { get; set; } = string.Empty;

        public string ShipCity { get; set; } = string.Empty;

        public string ShipPostalCode { get; set; } = string.Empty;

        public string ShipCountry { get; set; } = string.Empty;

        public List<PikchaUserBaseDTO> Following { get; set; } = new List<PikchaUserBaseDTO>();

        public DateTimeOffset LastUploadedOn { get; set; } = DateTimeOffset.MinValue;

        public List<string> Roles { get; set; } = new List<string>();

    }


    public class PikchaUserBaseDTO
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string AvatarFileName { get; set; } = string.Empty;

    }

}
