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

        public long TotalImageViews { get; set; }

        public string TopImageTitle { get; set; }
        public string TopImageLocation { get; set; }
        public string TopImageThumbnailFile { get; set; }
        public string TopImageWatermarkedFile { get; set; }
        public long TopImageTotalViews { get; set; }



        //public PikchaUser Artist { get; set; }
        //public IEnumerable<PikchaImageTag> ImageTags { get; set; }

        // public List<ImageView> PikchaImageViews { get; set; }

        //public int PikchaImageViewsCount { get; set; }

    }


    public class PikchaLoggedInUserDTO : PikchaUserBaseDTO
    {
        public DateTimeOffset LastUploadedOn { get; set; }

        public bool IsPhotoGrapher { get; set; }
    }


    public class PikchaUserBaseDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

}
