using Microsoft.AspNetCore.Mvc;

namespace Radiao.Api.Controllers
{
    public abstract class MainController : ControllerBase
    {
        protected readonly ILogger Logger;

        protected MainController(ILogger logger)
        {
            Logger = logger;
        }

        protected int GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (claim == null)
            {
                return 0;
            }

            return int.Parse(claim.Value);
        }

        protected ActionResult CustomResponse(object? data = null)
        {
            return Ok(new
            {
                success = true,
                data
            });

        }
    }
}
