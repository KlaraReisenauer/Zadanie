using Microsoft.AspNetCore.Mvc;

namespace ZadanieAPI.Controllers
{
    [Route("/error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
