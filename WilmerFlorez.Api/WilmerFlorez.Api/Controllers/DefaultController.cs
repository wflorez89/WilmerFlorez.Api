using WilmerFlorez.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace WilmerFlorez.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
       
        public DefaultController()
        {
        }

        [HttpGet]
        public string Get()
        {
            return "Running...";
        }
    }
}
