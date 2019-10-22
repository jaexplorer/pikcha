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
                    opt => opt.MapFrom(src =>
                        src.Views.Sum(y => y.Count).ToString()))
                .ForMember(
                    dest => dest.Height, opt => opt.MapFrom( src => "0"))
                .ForMember(
                    dest => dest.ProductIds, opt => opt.MapFrom(src => src.Products.Select(p => p.Id)))
                ;

            CreateMap<PikchaUser, PikchaImageFilterDTO>()
                  .ForMember(
                      dest => dest.Artist,
                      opt => opt.MapFrom(src =>
                          new PikchaUserBaseDTO() { Avatar = src.Avatar, Id = src.Id, FName = src.FName, LName = src.LName }))
                  .ForMember(
                      dest => dest.Caption, opt => opt.MapFrom(src => src.TopImage.Caption))
                   .ForMember(
                      dest => dest.Height, opt => opt.MapFrom(src => "0"))
                   .ForMember(
                      dest => dest.Location, opt => opt.MapFrom(src => src.TopImage.Location))
                   .ForMember(
                      dest => dest.Id, opt => opt.MapFrom(src => src.TopImage.Id))
                   .ForMember(
                      dest => dest.Thumbnail, opt => opt.MapFrom(src => src.TopImage.Thumbnail))
                   .ForMember(
                      dest => dest.Title, opt => opt.MapFrom(src => src.TopImage.Title))
                   .ForMember(
                      dest => dest.TotSold, opt => opt.MapFrom(src => 0))
                   .ForMember(
                      dest => dest.Watermark, opt => opt.MapFrom(src => src.TopImage.Watermark))

                   ;


                //CreateMap<PikchaImage, Pikcha100ImageDTO>()
                //    .ForMember(
                //    dest => dest.TotalViews,
                //    opt => opt.MapFrom(src =>
                //        src.Views.Sum(y => y.Count)))
                //    .ForMember(
                //    dest => dest.Height, opt => opt.MapFrom(src => "0"))
                //   // .ForAllMembers(opt => opt.NullSubstitute(string.Empty))
                //    ;

                //CreateMap<PikchaImage, PikchaImageDescriptionDTO>()
                //    .ForMember(
                //    dest => dest.TotalViews,
                //    opt => opt.MapFrom(src =>
                //        src.Views.Sum(y => y.Count)))
                //.ForAllMembers(opt => opt.NullSubstitute(string.Empty))

                ;
            //CreateMap<PikchaUser, PikchaImageBaseDTO>()
            //.ForAllMembers(opt => opt.NullSubstitute(string.Empty));
        }

        private void InitUserDTOs()
        {

            CreateMap<PikchaUser, PikchaUserBaseDTO>()
                 .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => src.City + ", " + src.Country))
                //.ForAllMembers(opt => opt.NullSubstitute(string.Empty))
                ;

            CreateMap<PikchaUser, PikchaArtistDTO>()
                .ForMember(
                dest => dest.Followers,
                opt => opt.MapFrom(src => src.Following.Select(y => y.PikchaUser).ToList()))

                 .ForMember(
                dest => dest.Following,
                opt => opt.MapFrom(src => src.Following.Select(y => y.PikchaArtist).ToList()))
                //.ForAllMembers(opt => opt.NullSubstitute(string.Empty))
                ;

            //CreateMap<PikchaUser, PikchaArtistDTO>()
            //.ForAllMembers(opt => opt.NullSubstitute(new { }))

            /*.ForMember(
            dest => dest.Views,
            opt => opt.MapFrom(src =>
                //src.Images.Select(y => y.Views.Sum( p => p.Count)).Sum()))
                src.TopImage.Views.Sum(p => p.Count)))

            .ForMember(
            dest => dest.BestImageTotalViews,
            opt => opt.MapFrom(src =>
                src.TopImage.Views.Sum(p => p.Count))) */
            ;

            CreateMap<PikchaUser, PikchaAuthenticatedUserDTO>()
                .ForMember(
                dest => dest.LUploadOn,
                opt => opt.MapFrom(src => src.Images.OrderByDescending(i => i.UploadedAt).First() == null ? DateTime.MinValue : src.Images.OrderByDescending(i => i.UploadedAt).First().UploadedAt))

                 .ForMember(
                dest => dest.Following,
                opt => opt.MapFrom(src => src.Following.Select(y => y.PikchaArtist).ToList()))
                //.ForAllMembers(opt => opt.NullSubstitute(string.Empty))
                 ;

        }

        private void InitSaleDTOs()
        {
            CreateMap<ImageProduct, ImageProductDTO>()
                .ForMember(
                    dest => dest.Artist,
                    opt => opt.MapFrom(src =>
                        new PikchaUserBaseDTO() { Avatar = src.Image.Artist.Avatar, Id = src.Image.Artist.Id, FName = src.Image.Artist.FName, LName = src.Image.Artist.LName }))
                 .ForMember(
                      dest => dest.Caption, opt => opt.MapFrom(src => src.Image.Caption))
                 .ForMember(
                      dest => dest.Location, opt => opt.MapFrom(src => src.Image.Location))
                 .ForMember(
                      dest => dest.ImageId, opt => opt.MapFrom(src => src.Image.Id))
                 .ForMember(
                      dest => dest.Title, opt => opt.MapFrom(src => src.Image.Title))
                 .ForMember(
                      dest => dest.Views, opt => opt.MapFrom(src =>  src.Image.Views.Count()))
                ;

        }
    }

   
}
