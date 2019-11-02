using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaSaleDTOs
    {
    }

    public class ImageProductDTO
    {
        public string Id { get; set; } = string.Empty;
        public string ImageId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool IsSale { get; set; } = false;

        public decimal Price { get; set; } = 0;
        public decimal AvgPrice { get; set; } = 0;
        public string Watermark { get; set; } = string.Empty;
        public string Views { get; set; } = string.Empty;

        public decimal Performance { get; set; } = 0;
        public int TotSold { get; set; } = 0;
        public PikchaUserBaseDTO Artist { get; set; } = new PikchaUserBaseDTO();

        public List<ProductSellerDTO> Sellers { get; set; } = new List<ProductSellerDTO>();
    }

    public class ProductSellerDTO
    {
        public string Id { get; set; } = string.Empty;
        public string SellerId { get; set; } = string.Empty;
        public bool IsSale { get; set; } = false;

        public decimal Price { get; set; } = 0;

        public string Type { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling



    }
}
