using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PikchaWebApp.Models
{
    [Table("PikchaUsers")]
    public class PikchaUser : IdentityUser
    {
        // personal info
        [Column("Fname")]
        public string FName { get; set; }

        [Column("Lname")]
        public string LName { get; set; }

        [Column("Avatar")]
        public string Avatar { get; set; }

        [Column("Sign")]
        public string Sign { get; set; }

        [Column("InvSign")]
        public string InvSign { get; set; }

        [Column("Bio")]
        public string Bio { get; set; }
        
        
        // shipping address
        [Column("Addr1")]
        public string Addr1 { get; set; }

        [Column("Addr2")]
        public string Addr2 { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("Postal")]
        public string Postal { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("Links")]
        public Dictionary<string, string> Links { get; set; } 

        public List<PikchaImage> Images { get; set; }

        public List<ArtistFollower> Following { get; set; } = new List<ArtistFollower>();
        public List<ArtistFollower> Followers { get; set; } = new List<ArtistFollower>();

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        //public ICollection<IdentityUserRole<int>> Roles { get; } = new List<IdentityUserRole<int>>();

       /* [NotMapped]
        public PikchaImage TopImage
        {
            get
            {
                // get the pikcha images
                
                if(this.Images == null)
                {
                    
                    return new PikchaImage() { Title = "N/A", Location = "N/A" };
                }
                try
                {
                    return this.Images.OrderByDescending(v => v.Views.Sum(i => i.Count)).First();

                }
                
                catch(Exception ex)
                {
                    return new PikchaImage() { Title = "N/A", Location = "N/A" };
                }
            }
        }*/

        /*[NotMapped]
        public int AggrImViews
        {
            get
            {
                // get the pikcha images

                if (this.Images == null || this.Images.Select(v => v.Views) == null)
                {

                    return 0;
                }
                try
                {
                    return this.Images.Sum(v => v.Views.Sum(c => c.Count));

                }

                catch (Exception ex)
                {
                    return 0;
                }
            }
        } */
    }

    [Table("ArtistFollowers")]
    public class ArtistFollower
    {
        public string UserId { get; set; }

        public PikchaUser PikchaUser { get; set; }

        public string ArtistsId { get; set; }

        public PikchaUser Artist { get; set; }

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
