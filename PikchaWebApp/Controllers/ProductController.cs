using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PikchaWebApp.Data;
using PikchaWebApp.Managers;
using PikchaWebApp.Models;

namespace PikchaWebApp.Controllers
{
    public class ProductController : PikchaBaseController
    {
        protected readonly PikchaDbContext _pikchDbContext;
        private readonly IMapper _mapper;
        public ProductController(PikchaDbContext pikchDbContext, IMapper mapper)
        {
            _pikchDbContext = pikchDbContext;
            _mapper = mapper;
        }

        [HttpGet("api/product/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetById(string productId)
        {
            try
            {
                var imgProd = await _pikchDbContext.ImageProducts.Include("Image").Include("Image.Artist").FirstAsync(im => im.Id == productId);
                if (imgProd == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);

                }
                var image = _mapper.Map<ImageProductDTO>(imgProd);
                return ReturnOkOrErrorStatus(image);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);
            }

        }
    }
}