using PikchaWebApp.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaUserDTOs
    {
    }

    public class ArtistDTO : ArtistBaseDTO
    {
        public string Bio { get; set; } = string.Empty;
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();

        public decimal Performance { get; set; } = 0;

        public int TotSold { get; set; } = 0;
        public decimal AvgPrice { get; set; } = 0;
        
        public List<UserBaseDTO> Following { get; set; } = new List<UserBaseDTO>();
        public List<UserBaseDTO> Followers { get; set; } = new List<UserBaseDTO>();

        public string Cover { get; set; } = string.Empty;
        public string Sign { get; set; } = string.Empty;


    }


    public class AuthenticatedUserDTO : UserBaseDTO
    {
        public string Bio { get; set; } = string.Empty;
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();

        public string Addr1 { get; set; } = string.Empty;

        public string Addr2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Postal { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public List<UserBaseDTO> Following { get; set; } = new List<UserBaseDTO>();

        public DateTimeOffset LUploadOn { get; set; } = DateTimeOffset.MinValue;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new List<string>();


    }


    public class UserBaseDTO
    {
        public string Id { get; set; } = string.Empty;
        public string FName { get; set; } = string.Empty;

        public string LName { get; set; } = string.Empty;
        public string Avatar { get; set; } = PikchaConstants.PIKCHA_USER_DEFAULT_AVATAR;


    }

    public class ArtistBaseDTO : UserBaseDTO
    {
        public string Location { get; set; } = string.Empty;
        public string AggrImViews { get; set; } = "0";
    }
       

}
