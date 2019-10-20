using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    [Table("Images")]
    public class PikchaImage
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Caption")]
        public string Caption { get; set; }

        [Column("Location")]
        public string Location { get; set; }

        [Column("Width")]
        public int Width { get; set; }

        [Column("Height")]
        public int Height { get; set; }

        [Column("Thumbnail")]
        public string Thumbnail { get; set; }

        [Column("Watermark")]
        public string Watermark { get; set; }

        [Column("UploadedAt")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset UploadedAt { get; set; }

        [Column("ModifiedAt")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset ModifiedAt { get; set; }


        [Column("Artist")]
        public PikchaUser Artist { get; set; }
        
        public IEnumerable<PikchaImageTag> Tags { get; set; }
        public IEnumerable<PikchaImageViews> Views { get; set; }
        public IEnumerable<ImageProduct> Products { get; set; }

        
    }

    [Table("PikchaTags")]
    public class PikchaTag
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public IEnumerable<PikchaImageTag> Tags { get; set; }
    }

    [Table("Tags")]
    public class PikchaImageTag
    {
        public string ImageTagId { get; set; }
        
        public PikchaTag ImageTag { get; set; }

        public string PikchaImageId { get; set; }
        
        public PikchaImage PikchaImage { get; set; }

    }

    [Table("Views")]
    public class PikchaImageViews
    {
        [Column(TypeName = "Date")]
        [Required]
        public DateTime Date { get; set; }
        
        public int Count { get; set; }

        public string PikchaImageId { get; set; }

        public PikchaImage PikchaImage { get; set; }

    }

}
