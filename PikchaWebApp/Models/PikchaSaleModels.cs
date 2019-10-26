using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaSaleModels
    {
    }

    [Table("ImageProducts")]
    public class ImageProduct
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        //[Column("ProdCode")]
        //public string ProdCode { get; set; }

        //[Column("ProdNum")]
        //public int ProdNum { get; set; }
        //// https://docs.microsoft.com/en-us/ef/core/modeling/relational/sequences

        [Column("IsSale")]
        public bool IsSale { get; set; } = false;

        [Column("Price", TypeName = "DECIMAL(13,2)")]
        public decimal Price { get; set; } = 0;

        [Column("Type")]
        public string Type { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling

        [Column("Image")]
        public PikchaImage Image { get; set; }

        [Column("Seller")]
        public PikchaUser Seller { get; set; }
    }


}
