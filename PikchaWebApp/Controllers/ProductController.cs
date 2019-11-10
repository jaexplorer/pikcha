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


        [HttpGet("{printerCode}/templates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTemplates(string printerCode)
        {
            try
            {
                PrinterManager printManager = new PrinterManager(_clientFactory, printerCode);
                List<ProductTemplate> templates = await printManager.GetProductTemplates();

                return ReturnOkOrErrorStatus(templates);
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Product, GetTemplates, parameters: printerCode={printerCode}", printerCode);

                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);

            }
        }



        [HttpGet("{printerCode}/quote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetQuote()
        {
            try
            {
                QuoteRequest quoteRequest = new QuoteRequest();
                PrinterManager printManager = new PrinterManager(_clientFactory, "JONDO");
                QuoteResult quoteReslt = await printManager.GetQuote(quoteRequest);

                return ReturnOkOrErrorStatus(quoteReslt);
            }
            catch (Exception ex)
            {
                // TO DO : need to add params
                Log.Error(ex, " Product, GetQuote, parameters: ");

                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);

            }
        }


        [HttpGet("{printerCode}/order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder()
        {
            try
            {
                OrderRequest orderRequest = new OrderRequest();
                PrinterManager printManager = new PrinterManager(_clientFactory, "JONDO");
                OrderResult quoteReslt = await printManager.CreateOrder(orderRequest);

                return ReturnOkOrErrorStatus(quoteReslt);
            }
            catch (Exception ex)
            {
                // TO DO : need to add params
                Log.Error(ex, " Product, CreateOrder, parameters: ");

                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ProductNotFound);

            }
        }


    }


}