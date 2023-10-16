using Microsoft.AspNetCore.Mvc;

namespace TaskTideAPI.Controllers
{
    public class CalendarController : AuthorizedController
    {
        [HttpGet]
        public IActionResult List()
        {
            return this.Ok(
                new
                {

                }
            );
        }
    }
}
