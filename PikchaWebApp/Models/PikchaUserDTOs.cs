using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaUserDTOs
    {
    }

    public class Pikcha100ArtistDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string PerCountry { get; set; }

        public long TotalImageViews { get; set; }

        public string BestImageTitle { get; set; }
        public string BestImageLocation { get; set; }
        public string BestImageThumbnailFile { get; set; }
        public string BestImageWatermarkedFile { get; set; }
        public long BestImageTotalViews { get; set; }



        //public PikchaUser Artist { get; set; }
        //public IEnumerable<PikchaImageTag> ImageTags { get; set; }

        // public List<ImageView> PikchaImageViews { get; set; }

        //public int PikchaImageViewsCount { get; set; }

    }

}
