using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    [Table("PikchaUsers")]
    public class PikchaUser : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PikchaUserId")]
        public int PikchaUserId { get; set; }
        // personal info
        [Column("Fname")]
        public string FirstName { get; set; }

        [Column("Lname")]
        public string LastName { get; set; }

        [Column("AvFile")]
        public string AvatarFileName { get; set; }

        [Column("SigFile")]
        public string SignatureFileName { get; set; }

        [Column("Bio")]
        public string BioInfo { get; set; }


        // permenant address
        [Column("PerAddr1")]
        public string PerAddress1 { get; set; }

        [Column("PerAddr2")]
        public string PerAddress2 { get; set; }

        [Column("PerCity")]
        public string PerCity { get; set; }

        [Column("PerPostal")]
        public string PerPostalCode { get; set; }

        [Column("PerCountry")]
        public string PerCountry { get; set; }


        // shipping address
        [Column("ShipAddr1")]
        public string ShipAddress1 { get; set; }

        [Column("ShipAddr2")]
        public string ShipAddress2 { get; set; }

        [Column("ShipCity")]
        public string ShipCity { get; set; }

        [Column("ShipPostal")]
        public string ShipPostalCode { get; set; }

        [Column("ShipCountry")]
        public string ShipCountry { get; set; }

        [Column("SocialLinks")]
        public Dictionary<string, string> SocialLinks { get; set; } 

        public List<PikchaImage> PikchaImages { get; set; }

        public List<PikchaArtistFollower> FollowingArtists { get; set; } = new List<PikchaArtistFollower>();
        public List<PikchaArtistFollower> Followers { get; set; } = new List<PikchaArtistFollower>();

        [NotMapped]
        public PikchaImage TopImage
        {
            get
            {
                // get the pikcha images
                
                if(this.PikchaImages == null)
                {
                    
                    return new PikchaImage() { Title = "N/A", Location = "N/A" };
                }
                try
                {
                    return this.PikchaImages.OrderByDescending(v => v.PikchaImageViews.Sum(i => i.Count)).First();

                }
                
                catch(Exception ex)
                {
                    return new PikchaImage() { Title = "N/A", Location = "N/A" };
                }
            }
        }
    }


    [Table("PikchaArtistFollowers")]
    public class PikchaArtistFollower
    {
        public string UserId { get; set; }

        public PikchaUser PikchaUser { get; set; }

        public string ArtistsId { get; set; }

        public PikchaUser PikchaArtist { get; set; }

    }




    //public class 
    [Table("PikchaRoles")]
    public class PikchaRole: IdentityRole<string>
    {

    }

    [Table("PikchaRoleClaims")]
    public class PikchaRoleClaim : IdentityRoleClaim<string>
    {

    }

    [Table("PikchaUserClaims")]
    public class PikchaUserClaim : IdentityUserClaim<string>
    {

    }

    [Table("PikchaUserLogins")]
    public class PikchaUserLogin : IdentityUserLogin<string>
    {

    }
    [Table("PikchaUserRoles")]
    public class PikchaUserRole : IdentityUserRole<string>
    {

    }

    [Table("PikchaUserTokens")]
    public class PikchaUserToken : IdentityUserToken<string>
    {

    }

    


}
