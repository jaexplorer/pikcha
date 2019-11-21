using PikchaWebApp.Drivers.Printer;
using PikchaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PikchaWebApp.Managers
{
    public class PrinterManager
    {
        protected readonly PrinterDriver _printer;

        public PrinterManager(IHttpClientFactory clientFactory, string printerCode)
        {
            // if printerCode == JONDO
            _printer =  new JondoPrinterDriver(clientFactory);
        }

        public async Task<List<ProductTemplate>> GetProductTemplates()
        {
            return await _printer.GetProductTemplates();
        }

        public async Task<QuoteResult> GetQuote(QuoteRequest quoteRequest)
        {
            return await _printer.GetQuote(quoteRequest);
        }

        public async Task<OrderResult> CreateOrder(OrderRequest orderRequest)
        {
            return await _printer.CreateOrder(orderRequest);
        }

        private QuoteRequest GenerateRequest(PikchaUser user, Dictionary<string, int> itms)
        {
                        
            QuoteRequest req = new QuoteRequest();
            req.Addr1 = user.Addr1;
            req.Addr2 = user.Addr2;
            req.City = user.City;
            req.Postal = user.Postal;
            req.State = user.State;

            foreach (var itm in itms)
            {
                req.QuoteItems.Add(new QuoteItem() { Code = itm.Key, Qty = itm.Value });

            }

            return req;

        }
    }
}
