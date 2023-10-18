using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskTideAPI.Extensions;

namespace TaskTideAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public abstract class BaseAuthorizedController : Controller
    {
        protected int CurrentUserId = 0;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.CurrentUserId = this.HttpContext.GetCurrentUserId();
        }
    }
}
