using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    [Table("PikchaImages")]
    public class PikchaImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public uint Id { get; set; }

        [Key]
        [Column("PikchaImageId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PikchaImageId { get; set; }

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
        
        public IEnumerable<PikchaImageTag> PikchaImageTags { get; set; }
        public IEnumerable<ImageView> PikchaImageViews { get; set; }

    }

    [Table("ImageTags")]
    public class ImageTag
    {
        [Key]
        [Column("ImageTagId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint ImageTagId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public IEnumerable<PikchaImageTag> PikchaImageTags { get; set; }
    }

    [Table("PikchaImageTags")]
    public class PikchaImageTag
    {
        [Key]
        [Column("PikchaImageTagId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint PikchaImageTagId { get; set; }
        public ImageTag ImageTag { get; set; }
        public PikchaImage PikchaImage { get; set; }

    }

    [Table("PikchaImageViews")]
    public class ImageView
    {
        [Key]
        [Column("PikchaImageViewId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint PikchaImageViewId { get; set; }


        [Column(TypeName = "Date")]
        [Required]
        public DateTime Date { get; set; }
        
        public uint Count { get; set; }

        public PikchaImage PikchaImage { get; set; }

    }

}
