using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikchaWebApp.Data;
using PikchaWebApp.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PikchaWebApp.Managers;
using Serilog;
using Microsoft.AspNetCore.Identity;

namespace PikchaWebApp.Controllers
{
    public class FilterController : PikchaBaseController
    {
        protected readonly PikchaDbContext _pikchDbContext;
        private readonly IMapper _mapper;

        public FilterController(PikchaDbContext pikchDbContext, IMapper mapper)
        {
            _pikchDbContext = pikchDbContext;
            _mapper = mapper;

        }

        [HttpGet("images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status416RangeNotSatisfiable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Images(string Type = "random", int Start = 0, int Count = 20, string ArtistId = "")
        {
            try
            {
                switch (Type)
                {
                    case "pikcha100":
                        return await ProcessPikcha100(Start, Count);
                    //break;
                    case "artist100":
                        return await ProcessArtists100(Start, Count);
                    case "artistId":
                        return await ProcessArtistsId(ArtistId, Start, Count);
                    default:
                        return await ProcessRandomImages(Start, Count);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Filter, Images, parameters: type={Type}, Start={Start}, Count={Count}", Type, Start, Count);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

            }
        }

        private async Task<ActionResult> ProcessPikcha100(int start, int count)
        {
            var pikcha100imgs = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaImages.Include("Views").OrderByDescending(i => i.Views.Count()).Skip(start).Take(count)).ToListAsync();
            return ReturnOkOrErrorStatus(pikcha100imgs);
        }

        private async Task<ActionResult> ProcessArtists100(int start, int count)
        {
            /*var artists100 = (from user in _pikchDbContext.PikchaUsers.Where(i => i.Images.Count > 0).OrderByDescending(i => i.Images.Sum(v => v.Views.Sum(c => c.Count))).Include( p => p.Images).ThenInclude(r => r.Products)
                                    select new PikchaArtist100ImageFilterDTO()
                                    {
                                        Artist = new ArtistBaseDTO()
                                        {
                                            Avatar = user.Avatar,
                                            FName = user.FName,
                                            LName = user.LName,
                                            Id = user.Id,
                                            Location = string.Concat(string.IsNullOrEmpty(user.City) ? string.Empty : user.City + ", ", string.IsNullOrEmpty(user.Country) ? string.Empty : user.Country),
                                            Email = user.Email,
                                            AggrImViews = (from img in user.Images
                                                           join iv in _pikchDbContext.ImageViews on img.Id equals iv.PikchaImage.Id
                                                           select iv
                                                        ).Sum(v => v.Count).ToString()
                                        },

                                        TopImage = _mapper.Map<PikchaImageFilterDTO>(user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First()),
                                        Views = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First().Views.Sum(v => v.Count).ToString(),
                                        ProductIds = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First().Products.Where(p => p.IsSale == true).OrderBy(p => p.Type).Select(p => p.Id).ToList()
                                    }).Skip(start).Take(count).ToList(); */

           /* var artists100 = (from user in _pikchDbContext.PikchaUsers.Where(i => i.Images.Count > 0).OrderByDescending(i => i.Images.Sum(v => v.Views.Sum(c => c.Count))).Include(p => p.Images).ThenInclude(r => r.Products)
                              select new PikchaArtist100ImageFilterDTO()
                              {
                                  Artist = new ArtistBaseDTO()
                                  {
                                      Avatar = user.Avatar,
                                      FName = user.FName,
                                      LName = user.LName,
                                      Id = user.Id,
                                      Location = string.Concat(string.IsNullOrEmpty(user.City) ? string.Empty : user.City + ", ", string.IsNullOrEmpty(user.Country) ? string.Empty : user.Country),
                                      Email = user.Email,
                                      AggrImViews = (from img in user.Images
                                                     join iv in _pikchDbContext.ImageViews on img.Id equals iv.PikchaImage.Id
                                                     select iv
                                                  ).Sum(v => v.Count).ToString()
                                  },


                                  TopImage = _mapper.Map<PikchaImageFilterDTO>(user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First()),


                                  Views = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First().Views.Sum(v => v.Count).ToString(),
                                  ProductIds = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First().Products.Where(p => p.IsSale == true).OrderBy(p => p.Type).Select(p => p.Id).ToList()


                              }).Skip(0).Take(20).ToList(); */

            var images = _pikchDbContext.PikchaUsers.Include("Images.Views").Include("Images.Artist")
                    .OrderByDescending(u => u.Images.Sum(i => i.Views.Sum(v => v.Count)))
                    .Skip(start).Take(count)
                    //.Select(u => u.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First())
                    .SelectMany(u => u.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).Take(1))
                    ;

            var artists100 = await _mapper.ProjectTo<PikchaImageFilterDTO>(images).ToListAsync();

            return ReturnOkOrErrorStatus(artists100);
            //return OK(artists100);
        }


        private async Task<ActionResult> ProcessArtistsId(string artistId, int start, int count)
        {
            if (artistId == "")
            {
                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ArtistNotFound);
            }
            var artists100 = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaImages.Include("Artist").Include("Views").Where(i => i.Artist.Id == artistId).AsQueryable().Skip(start).Take(count)).ToListAsync();
            return ReturnOkOrErrorStatus(artists100);
        }

        private async Task<ActionResult> ProcessRandomImages(int start, int count)
        {
            var images = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaImages.Skip(start).Take(count)).ToListAsync();
            return ReturnOkOrErrorStatus(images);
        }
    }

}