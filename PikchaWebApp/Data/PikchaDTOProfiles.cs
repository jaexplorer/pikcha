﻿using PikchaWebApp.Managers;
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
                    opt => opt.MapFrom(src => src.Views.Count()== 0 ? "0.1 k" : src.Views.Sum(y => y.Count).ToHumanReadableNumber()))
                .ForMember(
                    dest => dest.Height, opt => opt.MapFrom( src => "0"))
                .ForMember(
                    dest => dest.ProductIds, opt =>  opt.MapFrom(src => src.Products.Where(p => p.IsSale == true).OrderBy(p => p.Type).Select(p => p.Id)))
                 .ForMember(
                    dest => dest.TotSold, opt => opt.MapFrom(src => src.Products.Sum(p => p.InvoiceItems.Count())))
                 //.ForMember(
                 //   //dest => dest.AvgPrice, opt => opt.MapFrom(src => src.Products.Average(p => p.InvoiceItems.Sum( i => i.Qty * i.UnitPrice))))
                 //   dest => dest.AvgPrice, opt => opt.MapFrom(src =>  string.Format(new System.Globalization.CultureInfo("en-AU"), "{0:C}", src.Products.Average(p => p.InvoiceItems.Sum( i => i.Qty * i.UnitPrice))> 0 ? src.Products.Average(p => p.InvoiceItems.Sum(i => i.Qty * i.UnitPrice)): 0 )))

                 .ForMember(
                    //dest => dest.AvgPrice, opt => opt.MapFrom(src => src.Products.Average(p => p.InvoiceItems.Sum( i => i.Qty * i.UnitPrice))))
                    dest => dest.MinPrice, opt => opt.MapFrom(src => string.Format(new System.Globalization.CultureInfo("en-US"), "{0:C}", src.Products.Where(p => p.IsSale ==true).Min(p => p.FinPrice) > 0 ? src.Products.Where(p => p.IsSale == true).Min(p => p.FinPrice) : 0)))

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
                dest => dest.AggrImViews,
                opt => opt.MapFrom(src => src.Images.Select(v => v.Views).Count()>0? src.Images.Sum(i => i.Views.Sum(v => v.Count)).ToHumanReadableNumber() : "0.1 k"))
            ;

            CreateMap<PikchaUser, ArtistDTO>()
                .ForMember(
                dest => dest.Followers,
                opt => opt.MapFrom(src => src.Followers.Select(y => y.PikchaUser)))

                 .ForMember(
                dest => dest.Following,
                opt => opt.MapFrom(src => src.Following.Select(y => y.Artist)))

                .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => string.Concat(string.IsNullOrEmpty(src.City) ? string.Empty : src.City + ", ", string.IsNullOrEmpty(src.Country) ? string.Empty : src.Country)))

                .ForMember(
                dest => dest.AggrImViews,
                opt => opt.MapFrom(src => src.Images.Select(v => v.Views).Count() > 0 ? src.Images.Sum(i => i.Views.Sum(v => v.Count)).ToHumanReadableNumber() : "0.1 k"))
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
