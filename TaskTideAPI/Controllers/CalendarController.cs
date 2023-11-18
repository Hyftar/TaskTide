using Microsoft.AspNetCore.Mvc;
using TaskTideAPI.DataContexts;
using TaskTideAPI.DTO;
using TaskTideAPI.Models;
using TaskTideAPI.Repositories;
using NodaTime;

namespace TaskTideAPI.Controllers
{
    public class CalendarController : BaseAuthorizedController
    {
        private readonly TaskTideContext DbContext;
        private readonly ICalendarRepository CalendarRepository;
        private readonly ITransactionEventsRepository TransactionEventsRepository;

        public CalendarController(
            TaskTideContext dbContext,
            ICalendarRepository calendarRepository,
            ITransactionEventsRepository transactionEventsRepository)
        {
            this.DbContext = dbContext;
            this.CalendarRepository = calendarRepository;
            this.TransactionEventsRepository = transactionEventsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userCalendars = this.CalendarRepository.GetUserCalendars(this.CurrentUserId);

            return this.Json(userCalendars);
        }

        [HttpGet]
        public IActionResult Details(int calendarId)
        {
            var calendar = this.CalendarRepository.GetCalendar(this.CurrentUserId, calendarId);

            if (calendar == null)
            {
                return this.NotFound(new { Errors = new { CalendarId = new[] { "Calendar not found" } } });
            }

            return this.Json(calendar);
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
    }
}
