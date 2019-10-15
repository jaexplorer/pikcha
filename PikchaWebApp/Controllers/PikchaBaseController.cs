using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikchaWebApp.Managers;

namespace PikchaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PikchaBaseController : ControllerBase
    {

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status416RequestedRangeNotSatisfiable)]
        protected ActionResult ReturnOkOrNotFound(object value)
        {
            if(value == null)
            {
                return StatusCode(StatusCodes.Status416RequestedRangeNotSatisfiable, PikchaMessages.MESS_Status416RequestedRangeNotSatisfiable);
                //return NotFound();
            }
            var enumerableObj = value as System.Collections.IList;
            if (enumerableObj != null && enumerableObj.Count ==0)
            {
                return StatusCode(StatusCodes.Status416RequestedRangeNotSatisfiable, PikchaMessages.MESS_Status416RequestedRangeNotSatisfiable);

                //return NotFound();
            }
            return Ok(value);
        }

       
    }
}