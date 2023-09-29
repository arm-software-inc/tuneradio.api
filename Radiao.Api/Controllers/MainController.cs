using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Api.Controllers
{
    public abstract class MainController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly INotifier _notifier;

        protected MainController(
            ILogger logger,
            INotifier notifier)
        {
            Logger = logger;
            _notifier = notifier;
        }

        protected MainController(ILogger<AuthController> logger)
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
            if (HasErrors)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = _notifier.GetErrors().Select(e => e.Message)
                });
            }

            return Ok(new
            {
                success = true,
                data
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            return BadRequest(new
            {
                success = false,
                errors =  erros?.Select(e => e.ErrorMessage)
            });
        }

        protected bool HasErrors => _notifier.HasErrors();

        protected void Notify(string errorMessage)
        {
            _notifier.Handle(new Notification(errorMessage));
        }
    }
}
