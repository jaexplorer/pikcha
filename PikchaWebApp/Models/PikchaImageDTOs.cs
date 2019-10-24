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
    //public class PikchaRandomImageDTO : PikchaImageBaseDTO
    //{
    //    public string Caption { get; set; } = string.Empty;
    //    public string Height { get; set; } = string.Empty;

    //    public decimal Performance { get; set; } = 0;

    //    public int TotalPhotosSold { get; set; } = 0;
    //    public decimal AvgPrice { get; set; } = 0;

    //    public List<PikchaUserBaseDTO> Sellers { get; set; } = new List<PikchaUserBaseDTO>();
    //}


    //public class Pikcha100ImageDTO : PikchaImageBaseDTO
    //{
    //    public string Caption { get; set; } = string.Empty;
    //    public string Height { get; set; } = string.Empty;

    //    public int TotSold { get; set; } = 0;
    //    public decimal AvgPrice { get; set; } = 0;

    //    public List<PikchaUserBaseDTO> Sellers { get; set; } = new List<PikchaUserBaseDTO>();
    //}


    //public class PikchaImageDescriptionDTO : PikchaImageBaseDTO
    //{
    //    public string Caption { get; set; } = string.Empty;

    //    public int NumberOfPrint { get; set; } = 0;
    //    public int Width { get; set; } = 0;
    //    public int Height { get; set; } = 0;

    //}

    public class PikchaImageFilterDTO : PikchaImageBaseDTO
    {
        public string Caption { get; set; } = string.Empty;
        public decimal Performance { get; set; } = 0;
        public int TotSold { get; set; } = 0;
        public decimal AvgPrice { get; set; } = 0;

        public string Height { get; set; } = string.Empty;

        public List<string> ProductIds { get; set; } = new List<string>();

    }
    public class PikchaImageBaseDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Watermark { get; set; } = string.Empty;
        public string Views { get; set; } =  string.Empty;

        public PikchaArtistBaseDTO Artist { get; set; } = new PikchaArtistBaseDTO();

    }

}
