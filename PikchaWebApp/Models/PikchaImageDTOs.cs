using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaImageDTOs
    {

    }

    public class PikchaImageDTO
    {
        public uint Id { get; set; }
        public string PikchaImageId { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Location { get; set; }
        public int NumberOfPrint { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ThumbnailFile { get; set; }
        public string WatermarkedFile { get; set; }
        public DateTimeOffset UploadedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        //public PikchaUser Artist { get; set; }
        //public IEnumerable<PikchaImageTag> ImageTags { get; set; }

        // public List<ImageView> PikchaImageViews { get; set; }

        //public int PikchaImageViewsCount { get; set; }
        public long TotalViews { get; set; }

    }


    public class Pikcha100ImageDTO
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string ThumbnailFile { get; set; }
        public string WatermarkedFile { get; set; }
        public long TotalViews { get; set; }



        //public PikchaUser Artist { get; set; }
        //public IEnumerable<PikchaImageTag> ImageTags { get; set; }

        // public List<ImageView> PikchaImageViews { get; set; }

        //public int PikchaImageViewsCount { get; set; }

    }


    
}
