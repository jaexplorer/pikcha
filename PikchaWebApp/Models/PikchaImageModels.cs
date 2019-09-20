using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public uint Id { get; set; }

        [Key]
        [Column("ImageId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ImageId { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Caption")]
        public string Caption { get; set; }

        [Column("Location")]
        public string Location { get; set; }

        [Column("NoOfPrint")]
        public int NumberOfPrint { get; set; }

        [Column("Width")]
        public int Width { get; set; }

        [Column("Height")]
        public int Height { get; set; }

        [Column("ThumbFile")]
        public string ThumbnailFile { get; set; }

        [Column("WaterFile")]
        public string WatermarkedFile { get; set; }

        [Column("UploadedAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset UploadedAt { get; set; }

        [Column("ModifiedAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset ModifiedAt { get; set; }


        [Column("Artist")]
        public PikchaUser Artist { get; set; }


        public IEnumerable<ImageTag> Tags { get; set; }
    }

    public class ImageTag
    {
        [Key]
        [Column("ImageTagId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint ImageTagId { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }

  
}
