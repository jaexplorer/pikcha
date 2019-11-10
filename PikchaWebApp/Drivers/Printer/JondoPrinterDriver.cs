using PikchaWebApp.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PikchaWebApp.Drivers.Printer
{
    public class JondoPrinterDriver : PrinterDriver
    {
        string JONDO_DPQ_URL = "https://staging.jondohd.com/integration/api/dpqApiV2.php";
        string JONDO_CONFIG = "Drivers/Printer/JondoPrinterConfig.json";

        private List<ProductTemplate> _productTemplates;

        public JondoPrinterDriver(IHttpClientFactory clientFactory) : base(clientFactory)
        {
          
        }

        public override async Task<List<ProductTemplate>> GetProductTemplates()
        {
            return await GetTemplates();
        }

       

        public override async Task<QuoteResult> GetQuote(QuoteRequest quoteRequest)
        {

            return GenerateDummyQuoteResult(quoteRequest);
            /*
            QuoteResult qtResult = new QuoteResult();
            var request = new HttpRequestMessage(HttpMethod.Post, JONDO_DPQ_URL);
            //request.Headers.Add("Accept", "application/vnd.github.v3+json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            //request.Properties
            var client = _clientFactory.CreateClient();

            string xmlRequest = CreateDPQXMLRequest(quoteRequest);
            var bodyContent = new { xml = xmlRequest };

            request.Content = new StringContent(bodyContent.ToString(), Encoding.UTF8, "application/xml");

            var response = await client.SendAsync(request);
            //requestMessage.Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}", Encoding.UTF8, "application/json");

            if (response.IsSuccessStatusCode)
            {
                string resContent = await response.Content.ReadAsStringAsync();
                XDocument xDoc = XDocument.Parse(resContent);

                ParseDPQXMLResponse(xDoc, ref qtResult);
                //Branches = await response.Content
                //  .ReadAsAsync<IEnumerable<GitHubBranch>>();
            }
            else
            {
                //GetBranchesError = true;
               // Branches = Array.Empty<GitHubBranch>();
            }

            return qtResult; */


        }

        public override async Task<OrderResult> CreateOrder(OrderRequest orderRequest)
        {
            return GenerateDummyOrderResult(orderRequest);
        }


        private QuoteResult GenerateDummyQuoteResult(QuoteRequest quoteRequest)
        {
            //string reqCode = quoteRequest[]
            List<QuoteResultItem> resItms = new List<QuoteResultItem>();
            resItms.Add(new QuoteResultItem()
            {
                BaseFreight = "7.65",
                Carrier = "Fedex",
                EstDeliveryOn = DateTime.Now.AddDays(5).ToShortDateString(),
                Name = "Fedex International Priority",
                Tax = "0",
                TotFreight = "7.65",
                Type = "intPriority"
            });

            resItms.Add(new QuoteResultItem()
            {
                BaseFreight = "6.05",
                Carrier = "Fedex",
                EstDeliveryOn = DateTime.Now.AddDays(8).ToShortDateString(),
                Name = "Fedex International Economy",
                Tax = "0",
                TotFreight = "6.05",
                Type = "intEconomy"
            });

            resItms.Add(new QuoteResultItem()
            {
                BaseFreight = "7.05",
                Carrier = "USPS",
                EstDeliveryOn = DateTime.Now.AddDays(6).ToShortDateString(),
                Name = "USPS Priority Mail International",
                Tax = "0",
                TotFreight = "7.05",
                Type = "priorityMailInternational"
            });

            QuoteResult result = new QuoteResult()
            {
                StatusCode = PikchaMessages.CODE_Status200_MessageRecievedSuccess,
                ErrorCode = "",
                QuoteResults = resItms
            };

            return result;
        }


        private OrderResult GenerateDummyOrderResult(OrderRequest quoteRequest)
        {
            

            OrderResult result = new OrderResult()
            {
                StatusCode = PikchaMessages.CODE_Status200_MessageRecievedSuccess,
                ErrorCode = "",
            };

            return result;
        }


        private async Task<List<ProductTemplate>> GetTemplates()
        {
            if(_productTemplates == null)
            {
                using (FileStream fs = File.OpenRead(JONDO_CONFIG))
                {
                    _productTemplates = await JsonSerializer.DeserializeAsync<List<ProductTemplate>>(fs);
                }
            }
            
            return _productTemplates;
            
        }

        private string CreateDPQXMLRequest(QuoteRequest quoteRequest)
        {

            // craete quote items
            List<XElement> items = new List<XElement>();

            foreach(var itm in  quoteRequest.QuoteItems)
            {
                items.Add(new XElement("quoteItem",
                    new XElement("code", itm.Code),
                    new XElement("qt", itm.Qty)));
            }

            XDocument srcTree = new XDocument(
                new XComment("Quote request to JONDO"),
                new XElement("root",
                    new XElement("quoteRequest",
                    new XElement("userId", "99"),
                    new XElement("apiKey", "zz00zz"),
                    new XElement("refNumber", "123444"),
                    new XElement("address", quoteRequest.Addr1),
                    new XElement("address2", quoteRequest.Addr2),
                    new XElement("aptNumber", "123444"),
                    new XElement("city", quoteRequest.City),
                    new XElement("state", quoteRequest.State),
                    new XElement("zip", quoteRequest.Postal),
                    new XElement("urbanizationCode", "123444"),
                    new XElement("country", quoteRequest.Country),
                    new XElement("quoteItems", items)
                    
                    )
                    )
                );


            return srcTree.Root.ToString();
        }
    
    
        private QuoteResult ParseDPQXMLResponse(XDocument xDocmnt, ref QuoteResult quoteResult)
        {

            // get fedex
            ParseFedexResult(xDocmnt, ref quoteResult);

            // get usps

            ParseUSPSResult(xDocmnt, ref quoteResult);


            return quoteResult;

            //var results = xDocmnt.Descendants("fedex")
            //       .Select(r => new QuoteResultItem()
            //       {
            //           Carrier = "fedex",
            //           Name = r.Element("displayName").Value,
            //           Type = ""
            //           MyPath = (string)v.Attribute("path")
            //       });.ToList();

        }


        private QuoteResult ParseFedexResult(XDocument xDocmnt, ref QuoteResult quoteResult)
        {
            IEnumerable<XElement> fedChildNodes = xDocmnt.Element("fedex").Descendants();
            // check the error code
            string fedRes = xDocmnt.Element("fedex").Element("resp").Value;
            quoteResult.StatusCode = fedRes;
            if (fedRes != "0")
            {
                quoteResult.ErrorCode = xDocmnt.Element("fedex").Element("errorCode").Value;

                quoteResult.ErrorMessage = xDocmnt.Element("fedex").Element("errorMessage").Value;

                return quoteResult;
                //return quoteRes;
            }
            foreach (var elm in fedChildNodes)
            {

                quoteResult.QuoteResults.Add(new QuoteResultItem()
                {
                    Type = (string)elm.Name.LocalName,
                    Carrier = "fedex",
                    Name = elm.Element("displayName").Value,
                    EstDeliveryOn = elm.Element("deliveryEstimate").Value,
                    BaseFreight = elm.Element("baseFreight").Value,
                    Tax = elm.Element("tax").Value,
                    TotFreight = elm.Element("totalFreight").Value,
                    ErrorCode = elm.Element("displayName").Value,
                    ErrorMessage = elm.Element("displayName").Value

                });
            }

            return quoteResult;

        }


        private QuoteResult ParseUSPSResult(XDocument xDocmnt, ref QuoteResult quoteResult)
        {
            IEnumerable<XElement> fedChildNodes = xDocmnt.Element("usps").Descendants();
            // check the error code
            string fedRes = xDocmnt.Element("usps").Element("resp").Value;
            quoteResult.StatusCode = fedRes;
            if (fedRes != "0")
            {
                quoteResult.ErrorCode = xDocmnt.Element("usps").Element("errorCode").Value;

                quoteResult.ErrorMessage = xDocmnt.Element("usps").Element("errorMessage").Value;

                return quoteResult;
                //return quoteRes;
            }
            foreach (var elm in fedChildNodes)
            {

                quoteResult.QuoteResults.Add(new QuoteResultItem()
                {
                    Type = (string)elm.Name.LocalName,
                    Carrier = "usps",
                    Name = elm.Element("displayName").Value,
                    EstDeliveryOn = elm.Element("deliveryEstimate").Value,
                    BaseFreight = elm.Element("baseFreight").Value,
                    Tax = elm.Element("tax").Value,
                    TotFreight = elm.Element("totalFreight").Value,
                    ErrorCode = elm.Element("displayName").Value,
                    ErrorMessage = elm.Element("displayName").Value

                });
            }

            return quoteResult;

        }


        private string CreateCOFXMLRequest(OrderRequest orderRequest)

        {

            // craete quote items
            List<XElement> items = new List<XElement>();

            foreach (var itm in orderRequest.QuoteItems)
            {
                items.Add(new XElement("orderItem",
                    new XElement("code", itm.Code),
                    new XElement("qt", itm.Qty),
                    new XElement("imageLocation", itm.Qty)));
            }

            XDocument srcTree = new XDocument(
                new XComment("Order request to JONDO"),
                new XElement("root",
                    new XElement("orderRequest",
                    new XElement("userId", "99"),
                    new XElement("apiKey", "zz00zz"),
                    new XElement("shippingType", "123444"),
                    new XElement("testMode", "123444"),
                    new XElement("quoteId", "123444"),
                    new XElement("poNumber", "123444"),
                    new XElement("firstName", orderRequest.FirstName),
                    new XElement("lastName", orderRequest.LastName),
                    new XElement("company", orderRequest.Company),
                    new XElement("address", orderRequest.Addr1),
                    new XElement("address2", orderRequest.Addr2),
                    new XElement("aptNumber", "123444"),
                    new XElement("city", orderRequest.City),
                    new XElement("state", orderRequest.State),
                    new XElement("zip", orderRequest.Postal),
                    new XElement("urbanizationCode", "123444"),
                    new XElement("country", orderRequest.Country),
                    new XElement("phoneNumber", orderRequest.Country),
                    new XElement("email", orderRequest.Country),
                    // to do services
                    new XElement("orderItems", items)

                    )
                    )
                );


            return srcTree.Root.ToString();
        }

       
    }





}
