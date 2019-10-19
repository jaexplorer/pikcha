using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaImageDTOs 
    {

    }

    public class PikchaImageDTO : PikchaImageBaseDTO
    {

    }
    public class PikchaRandomImageDTO : PikchaImageBaseDTO
    {
        public string Caption { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;

        public decimal Performance { get; set; } = 0;

        public int TotalPhotosSold { get; set; } = 0;
        public decimal AveragePrice { get; set; } = 0;

        public List<PikchaUserBaseDTO> Sellers { get; set; } = new List<PikchaUserBaseDTO>();
    }


    public class Pikcha100ImageDTO : PikchaImageBaseDTO
    {
        public string Caption { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public decimal Performance { get; set; } = 0;

        public int TotalImageSold { get; set; } = 0;
        public decimal AveragePrice { get; set; } = 0;

        public List<PikchaUserBaseDTO> Sellers { get; set; } = new List<PikchaUserBaseDTO>();
    }


    public class PikchaImageDescriptionDTO : PikchaImageBaseDTO
    {
        public string Caption { get; set; } = string.Empty;

        public int NumberOfPrint { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

    }

    public class PikchaImageBaseDTO
    {
        public string PikchaImageId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ThumbnailFile { get; set; } = string.Empty;
        public string WatermarkedFile { get; set; } = string.Empty;
        public int TotalViews { get; set; } = 0;

        public PikchaUserBaseDTO Artist { get; set; } = new PikchaUserBaseDTO();

    }

}
