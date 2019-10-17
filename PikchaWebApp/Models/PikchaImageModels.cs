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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
                
        [Column("PikchaImageId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PikchaImageId { get; set; }

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

        [Column("ThumbFile")]
        public string ThumbnailFile { get; set; }

        [Column("WaterFile")]
        public string WatermarkedFile { get; set; }

        [Column("UploadedAt")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset UploadedAt { get; set; }

        [Column("ModifiedAt")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset ModifiedAt { get; set; }


        [Column("Artist")]
        public PikchaUser Artist { get; set; }
        
        public IEnumerable<PikchaImageTag> PikchaImageTags { get; set; }
        public IEnumerable<PikchaImageViews> PikchaImageViews { get; set; }

    }

    [Table("PikchaTags")]
    public class PikchaTag
    {
        [Key]
        [Column("ImageTagId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageTagId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public IEnumerable<PikchaImageTag> PikchaImageTags { get; set; }
    }

    [Table("PikchaImageTags")]
    public class PikchaImageTag
    {
        public int ImageTagId { get; set; }

        [ForeignKey("ImageTagId")]
        public PikchaTag ImageTag { get; set; }

        public int PikchaImageId { get; set; }

        [ForeignKey("PikchaImageId")]

        public PikchaImage PikchaImage { get; set; }

    }

    [Table("PikchaImageViews")]
    public class PikchaImageViews
    {
        [Key]
        [Column("PikchaImageViewId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PikchaImageViewId { get; set; }


        [Column(TypeName = "Date")]
        [Required]
        public DateTime Date { get; set; }
        
        public int Count { get; set; }

        public int PikchaImageId { get; set; }

        [ForeignKey("PikchaImageId")]
        public PikchaImage PikchaImage { get; set; }

    }

}
