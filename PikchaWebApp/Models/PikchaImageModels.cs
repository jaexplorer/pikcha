using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaImage
    {
        [Column("Title")]
        public string Title { get; set; }

        [Column("Caption")]
        public string Caption { get; set; }

        [Column("Location")]
        public string Location { get; set; }

        [Column("NoOfPrint")]
        public int NumberOfPrint { get; set; }

        [Column("Artist")]
        public PikchaUser Artist { get; set; }


        public IEnumerable<ImageTag> Tags { get; set; }
    }

    public class ImageTag
    {
        [Column("Name")]
        public string Name { get; set; }
    }

  
}
