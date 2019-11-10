using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PikchaWebApp.Drivers.Printer
{
    public abstract class PrinterDriver
    {
        protected readonly IHttpClientFactory _clientFactory;
        public PrinterDriver(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public abstract Task<List<ProductTemplate>> GetProductTemplates();
        public abstract Task<QuoteResult> GetQuote(QuoteRequest quoteRequest);
        public abstract Task<OrderResult> CreateOrder(OrderRequest orderRequest);


    }


    public class ProductTemplate
    {
        public string Code { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Material { get; set; }
        public string Frame { get; set; }
        public string Border { get; set; }
        public string Finish { get; set; }
        public int Qty { get; set; } = 0;
    }

    public class QuoteRequest
    {
        public string Addr1 { get; set; } = string.Empty;

        public string Addr2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Postal { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public List<QuoteItem> QuoteItems = new List<QuoteItem>();
    }


    
    public class QuoteItem
    {
        public string Code { get; set; } = string.Empty;
        public int Qty { get; set; } = 1;

    }
    public class QuoteResultItem
    {
        public string Carrier { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public string EstDeliveryOn { get; set; }

        public string BaseFreight { get; set; } = string.Empty;
        public string Tax { get; set; } = string.Empty;
        public string TotFreight { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

    }

    public class QuoteResult
    {
        public string StatusCode { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string ShippingClass { get; set; } = string.Empty;
        public string QuoteId { get; set; } = string.Empty;
        public string RefNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;


        public List<QuoteResultItem> QuoteResults { get; set; } = new List<QuoteResultItem>();

    }


    public class OrderRequest
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Addr1 { get; set; } = string.Empty;

        public string Addr2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Postal { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public List<OrderItem> QuoteItems = new List<OrderItem>();
    }

    public class OrderItem
    {
        public string Code { get; set; } = string.Empty;
        public int Qty { get; set; } = 1;

    }

    public class OrderResult
    {
        public string StatusCode { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
