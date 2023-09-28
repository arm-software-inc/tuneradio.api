using Microsoft.AspNetCore.Mvc;

namespace Radiao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationController : MainController
    {
        public StationController(ILogger logger) : base(logger)
        {
        }
    }
}
