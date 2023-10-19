using Microsoft.AspNetCore.Mvc;
using TaskTideAPI.DataContexts;
using TaskTideAPI.DTO;
using TaskTideAPI.Models;
using TaskTideAPI.Repositories;
using NodaTime;
using NodaTime.Extensions;

namespace TaskTideAPI.Controllers
{
    public class CalendarController : BaseAuthorizedController
    {
        private readonly TaskTideContext DbContext;
        private readonly ICalendarRepository CalendarRepository;
        private readonly ITransactionEventsRepository TransactionEventsRepository;
        private readonly IClock Clock;

        public CalendarController(
            TaskTideContext dbContext,
            ICalendarRepository calendarRepository,
            ITransactionEventsRepository transactionEventsRepository,
            IClock clock)
        {
            this.DbContext = dbContext;
            this.CalendarRepository = calendarRepository;
            this.TransactionEventsRepository = transactionEventsRepository;
            this.Clock = clock;
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
        public IActionResult AddTask([FromBody] AddTaskDTO addTaskDTO)
        {
            var calendar =
                this.DbContext
                    .Calendars
                    .SingleOrDefault(
                        x =>
                            x.Id == addTaskDTO.CalendarId
                            && x.Owner.Id == this.CurrentUserId
                    );

            if (calendar == null )
            {
                return this.NotFound(new { Errors = new { CalendarId = new[] { "Calendar not found" } } });
            }

            var taskEvent =
                new TaskEvent
                {
                    Parent = calendar,
                    Title = addTaskDTO.Title,
                    Description = addTaskDTO.Description,

                    CreatedAt = this.Clock.InUtc().GetCurrentZonedDateTime(),
                    Deleted = false,
                };

            var taskEventInstance =
                new TaskEventInstance
                {
                    AllDay = addTaskDTO.AllDay,
                    Parent = taskEvent,
                    StartDate = LocalDate.FromDateOnly(addTaskDTO.StartDate),
                    StartTime = addTaskDTO.StartTime is not null ? LocalTime.FromTimeOnly(addTaskDTO.StartTime.Value) : null,
                    Duration = addTaskDTO.DurationInMinutes is not null ? Duration.FromMinutes(addTaskDTO.DurationInMinutes.Value) : null,

                    CreatedAt = this.Clock.InUtc().GetCurrentZonedDateTime(),
                    IsCompleted = false,
                    Deleted = false,
                };

            var taskRecurrences =
                addTaskDTO
                    .Recurrences
                    .Select(
                        x =>
                            new Recurrence
                            {
                                Type = x.Type,
                                StartDate = LocalDate.FromDateOnly(x.StartDate),
                                Interval = x.Interval,
                                Months = x.Months,
                                Ordinal = x.Ordinal,
                                EndType = x.EndType,
                                Count = x.Count,
                                Duration = x.DurationInMinutes is not null ? Duration.FromMinutes(x.DurationInMinutes.Value) : null,
                                TaskEvent = taskEvent,
                                Weekdays = x.Weekdays,
                                Deleted = false,
                            }
                        )
                    .ToList();

            this.DbContext.Recurrences.AddRange(taskRecurrences);
            this.DbContext.TaskEventsInstance.Add(taskEventInstance);
            this.DbContext.TaskEvents.Add(taskEvent);

            var user = this.DbContext.Users.Single(x => x.Id == this.CurrentUserId);

            this.TransactionEventsRepository.Log(taskEvent, TransactionType.Create, user);
            this.TransactionEventsRepository.Log(taskRecurrences.First(), TransactionType.Create, user);
            this.TransactionEventsRepository.Log(taskEventInstance, TransactionType.Create, user);

            this.DbContext.SaveChanges();

            return this.Json(taskEvent);
        }
    }
}
