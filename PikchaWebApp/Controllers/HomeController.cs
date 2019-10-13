using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PikchaWebApp.Drivers.Email;

namespace PikchaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : PikchaBaseController
    {
        protected readonly IOptions<AuthMessageSenderOptions> _optionsAccessor;

        public HomeController(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;

        }

        [HttpPost("subscribefortesting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SubscribeForTesting(string Name, string Email)
        {
            EmailSender emailClient = new EmailSender(_optionsAccessor);

            string message = "Hi there, This message is generated from Pikcha website. One of our friends is willing to test pikcha website. His/ her details: Name : " + Name + ". Email : " + Email + ".  ";
            await emailClient.SendEmailAsync("stjeyan@gmail.com", "New friend subscribed Pikcha for testing", message);

            return Ok();
        }
    }
}