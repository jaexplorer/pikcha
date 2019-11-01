using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PikchaWebApp.Data;
using PikchaWebApp.Drivers.Printer;
using PikchaWebApp.Managers;
using PikchaWebApp.Models;
using Serilog;

namespace PikchaWebApp.Controllers
{
    public class ProductController : PikchaBaseController
    {
        protected readonly PikchaDbContext _pikchDbContext;
        private readonly IMapper _mapper;
        protected readonly UserManager<PikchaUser> _userManager;
        protected readonly IHttpClientFactory _clientFactory;

        public ProductController(PikchaDbContext pikchDbContext, IMapper mapper, UserManager<PikchaUser> userManager, IHttpClientFactory clientFactory)
        {
            _pikchDbContext = pikchDbContext;
            _mapper = mapper;
            _userManager = userManager;
            _clientFactory = clientFactory;
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetById(string productId)
        {
            try
            {
                var imgProd = await _pikchDbContext.ImageProducts.Include("Image").Include("Image.Views").Include("Image.Products.Seller").Include("Image.Artist").FirstAsync(im => im.Id == productId);
                if (imgProd == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);

                }
                var image = _mapper.Map<ImageProductDTO>(imgProd);
                return ReturnOkOrErrorStatus(image);
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Product, GetById, parameters: productId={productId}", productId);
                //return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);
            }

        }


        [HttpGet("quote/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetQuote(string userId)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);
                Dictionary<string, int> qtItems = new Dictionary<string, int>();
                qtItems.Add("1111", 1);
                qtItems.Add("1122", 2);
                qtItems.Add("1133", 3);

                PrinterManager printManager = new PrinterManager(_clientFactory);
                var quoteReq = await printManager.GetQuote(pikchaUser, qtItems);

                //_userManager.Users.First();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);

            }
        }

       

    }
}