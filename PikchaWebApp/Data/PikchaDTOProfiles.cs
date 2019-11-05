using PikchaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Data
{
    public class PikchaDTOProfiles : AutoMapper.Profile
    {

        public PikchaDTOProfiles()
        {
            InitImageDTOS();
            InitUserDTOs();
            InitSaleDTOs();
        }

        private void InitImageDTOS()
        {
            CreateMap<PikchaImage, PikchaImageFilterDTO>()
                .ForMember(
                    dest => dest.Views,
                    opt => opt.MapFrom(src => src.Views.Count()==0 ? "0" : src.Views.Sum(y => y.Count).ToString()))
                .ForMember(
                    dest => dest.Height, opt => opt.MapFrom( src => "0"))
                .ForMember(
                    dest => dest.ProductIds, opt =>  opt.MapFrom(src => src.Products.Where(p => p.IsSale == true).OrderBy(p => p.Type).Select(p => p.Id)))
                ;

            CreateMap<PikchaUser, PikchaArtist100ImageFilterDTO>()                  
                  .ForMember(
                      dest => dest.TopImage, opt => opt.MapFrom(src => src.Images.OrderByDescending(v => v.Views.Count()) .First()))
               
            ;
        }

        private void InitUserDTOs()
        {

            CreateMap<PikchaUser, UserBaseDTO>()              
                ;

            CreateMap<PikchaUser, ArtistBaseDTO>()
                 .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => string.Concat( string.IsNullOrEmpty( src.City) ? string.Empty : src.City + ", ", string.IsNullOrEmpty(src.Country) ? string.Empty : src.Country)))
                 .ForMember(
                dest => dest.Phone,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PhoneNumber) ? string.Empty : src.PhoneNumber))
                  .ForMember(
                dest => dest.AggrImViews,
                opt => opt.MapFrom(src => src.Images.Sum(i => i.Views.Sum(v => v.Count))))
            ;

            CreateMap<PikchaUser, ArtistDTO>()
                .ForMember(
                dest => dest.Followers,
                opt => opt.MapFrom(src => src.Following.Select(y => y.PikchaUser).ToList()))

                 .ForMember(
                dest => dest.Following,
                opt => opt.MapFrom(src => src.Following.Select(y => y.Artist).ToList()))
                ;

            CreateMap<PikchaUser, AuthenticatedUserDTO>()
               .ForMember(
                dest => dest.LUploadOn,
                opt => opt.MapFrom(src => src.Images.Count== 0 ? DateTimeOffset.MinValue : src.Images.OrderByDescending(i => i.UploadedAt).First().UploadedAt))
               
                 .ForMember(
                dest => dest.Following,
                opt => opt.MapFrom(src => src.Following.Select(y => y.Artist)))
                 ;

        }

        private void InitSaleDTOs()
        {
            CreateMap<ImageProduct, ImageProductDTO>()
                .ForMember(
                    dest => dest.Artist,
                    opt => opt.MapFrom(src =>
                        new UserBaseDTO() { Avatar = src.Image.Artist.Avatar?? string.Empty, Id = src.Image.Artist.Id, FName = src.Image.Artist.FName, LName = src.Image.Artist.LName }))
                 .ForMember(
                      dest => dest.Caption, opt => opt.MapFrom(src => src.Image.Caption))
                 .ForMember(
                      dest => dest.Location, opt => opt.MapFrom(src => src.Image.Location))
                 .ForMember(
                      dest => dest.ImageId, opt => opt.MapFrom(src => src.Image.Id))
                 .ForMember(
                      dest => dest.Title, opt => opt.MapFrom(src => src.Image.Title))
                 .ForMember(
                      dest => dest.Watermark, opt => opt.MapFrom(src => src.Image.Watermark))
                 .ForMember(
                      dest => dest.Views, opt => opt.MapFrom(src =>  src.Image.Views.Sum(v => v.Count).ToString()))
                 .ForMember(
                      dest => dest.Height, opt => opt.MapFrom(src => "0"))
                 .ForMember(
                      dest => dest.Sellers, opt => opt.MapFrom(src => src.Image.Products.Where(p=> p.IsSale ==true).Select(p => p)))
                ;


            CreateMap<ImageProduct, ProductSellerDTO>()
                 .ForMember(
                      dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id))
            ;

        }
    }

   
}
