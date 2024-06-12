using Microsoft.AspNetCore.Mvc;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealtController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"HealtController.Get = {DateTime.Now}");
        }
    }
}
