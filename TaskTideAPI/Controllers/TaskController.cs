using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Extensions;
using TaskTideAPI.DataContexts;
using TaskTideAPI.DTO;
using TaskTideAPI.Models;
using TaskTideAPI.Repositories;

namespace TaskTideAPI.Controllers
{
    public class TaskController : BaseAuthorizedController
    {
        private readonly TaskTideContext DbContext;
        private readonly ITransactionEventsRepository TransactionEventsRepository;
        private readonly ZonedClock Clock;

        public TaskController(
            TaskTideContext taskTideContext,
            ITransactionEventsRepository transactionEventsRepository,
            ZonedClock clock)
        {
            this.DbContext = taskTideContext;
            this.TransactionEventsRepository = transactionEventsRepository;
            this.Clock = clock;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddTaskDTO addTaskDTO)
        {
            var user = this.DbContext.Users.Single(x => x.Id == this.CurrentUserId);

            var calendar =
                this.DbContext
                    .Calendars
                    .SingleOrDefault(
                        x =>
                            x.Id == addTaskDTO.CalendarId
                            && x.Owner.Id == this.CurrentUserId
                    );

            if (calendar == null)
            {
                return this.NotFound(new { Errors = new { CalendarId = new[] { "Calendar not found" } } });
            }

            var taskEvent =
                new TaskEvent
                {
                    Parent = calendar,
                    Title = addTaskDTO.Title,
                    Description = addTaskDTO.Description,

                    CreatedAt = this.Clock.GetCurrentZonedDateTime(),
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

                    CreatedAt = this.Clock.GetCurrentZonedDateTime(),
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

            var lunarCalRecurrences =
                addTaskDTO
                    .LunarCalendarRecurrences
                    .Select(
                        x =>
                            new LunarCalendarRecurrence
                            {

                            }
                        )
                    .ToList();

            this.DbContext.LunarCalendarRecurrences.AddRange(lunarCalRecurrences);
            this.DbContext.Recurrences.AddRange(taskRecurrences);
            this.DbContext.TaskEventsInstance.Add(taskEventInstance);
            this.DbContext.TaskEvents.Add(taskEvent);

            this.TransactionEventsRepository.Log(taskEvent, TransactionType.Create, user);
            
            if (taskRecurrences.Any())
            {
                this.TransactionEventsRepository.Log(taskRecurrences.First(), TransactionType.Create, user);
            }
            
            if (lunarCalRecurrences.Any())
            {
                this.TransactionEventsRepository.Log(lunarCalRecurrences.First(), TransactionType.Create, user);
            }
            
            this.TransactionEventsRepository.Log(taskEventInstance, TransactionType.Create, user);

            this.DbContext.SaveChanges();

            return this.Json(taskEvent);
        }
    }
}
