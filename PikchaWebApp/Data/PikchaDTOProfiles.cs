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

        }

        private void InitImageDTOS()
        {
            CreateMap<PikchaImage, PikchaRandomImageDTO>()
                .ForMember(
                dest => dest.TotalViews,
                opt => opt.MapFrom(src =>
                    src.PikchaImageViews.Sum(y => y.Count)))
                .ForMember(
                dest => dest.Height, opt => opt.MapFrom( src => "0"))
                ;

            CreateMap<PikchaImage, Pikcha100ImageDTO>()
                .ForMember(
                dest => dest.TotalViews,
                opt => opt.MapFrom(src =>
                    src.PikchaImageViews.Sum(y => y.Count)))
                .ForMember(
                dest => dest.Height, opt => opt.MapFrom(src => "0"))
                ;

            CreateMap<PikchaImage, PikchaImageDescriptionDTO>()
                .ForMember(
                dest => dest.TotalViews,
                opt => opt.MapFrom(src =>
                    src.PikchaImageViews.Sum(y => y.Count)))
                ;

        }

        private void InitUserDTOs()
        {
            CreateMap<PikchaUser, Pikcha100ArtistDTO>()
                /*.ForMember(
                dest => dest.TotalImageViews,
                opt => opt.MapFrom(src =>
                    //src.PikchaImages.Select(y => y.PikchaImageViews.Sum( p => p.Count)).Sum()))
                    src.TopImage.PikchaImageViews.Sum(p => p.Count)))

                .ForMember(
                dest => dest.BestImageTotalViews,
                opt => opt.MapFrom(src =>
                    src.TopImage.PikchaImageViews.Sum(p => p.Count))) */
                ;            
            CreateMap<PikchaUser, PikchaLoggedInUserDTO>()
                .ForMember(
                dest => dest.LastUploadedOn,
                opt => opt.MapFrom(src => src.PikchaImages.OrderByDescending(i => i.UploadedAt).First() == null ? DateTime.MinValue : src.PikchaImages.OrderByDescending(i => i.UploadedAt).First().UploadedAt))
                
            ;
        }


    }

   
}
