using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PikchaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PikchaBaseController : ControllerBase
    {

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        protected ActionResult ReturnOkOrNotFound(object value)
        {
            if(value == null)
            {
                return NotFound();
            }
            var enumerableObj = value as System.Collections.IList;
            if (enumerableObj != null && enumerableObj.Count ==0)
            {
                return NotFound();
            }
            return Ok(value);
        }

       
    }
}