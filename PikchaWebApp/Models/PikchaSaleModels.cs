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

    [Table("Pk_ImageProducts")]
    public class ImageProduct
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("IsSale")]
        public bool IsSale { get; set; } = false;

        [Column("Price")]
        public decimal Price { get; set; } = 0;

        [Column("Type")]
        public string Type { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling

        [Column("Image")]
        public PikchaImage Image { get; set; }
    }


}
