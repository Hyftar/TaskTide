using Microsoft.AspNetCore.Mvc;
using TaskTideAPI.DataContexts;
using Microsoft.EntityFrameworkCore;
using TaskTideAPI.DTO;
using TaskTideAPI.Models;
using TaskTideAPI.Repositories;

namespace TaskTideAPI.Controllers
{
    public class CalendarController : BaseAuthorizedController
    {
        private readonly TaskTideContext DbContext;
        private readonly ICalendarRepository CalendarRepository;

        public CalendarController(
            TaskTideContext dbContext,
            ICalendarRepository calendarRepository)
        {
            this.DbContext = dbContext;
            this.CalendarRepository = calendarRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCalendarDTO calendarDTO)
        {
            var calendar =
                new Calendar
                {
                    Name = calendarDTO.Name,
                    Owner = this.DbContext.Users.Single(x => x.Id == this.CurrentUserId)
                };

            this.DbContext.Calendars.Add(calendar);
            this.DbContext.SaveChanges();

            return
                this.Json(
                    new
                    {
                        CalendarId = calendar.Id
                    }
                );
        }

        [HttpGet]
        public IActionResult List()
        {
            var userCalendars = this.CalendarRepository.GetUserCalendars(this.CurrentUserId);

            return this.Json(userCalendars);
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] AddTaskDTO taskDTO)
        {
            return
                this.Json(
                    new
                    {
                    }
                );
        }
    }
}
