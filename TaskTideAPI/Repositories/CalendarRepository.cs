using Microsoft.EntityFrameworkCore;
using TaskTideAPI.DataContexts;
using TaskTideAPI.Models;

namespace TaskTideAPI.Repositories
{
    public interface ICalendarRepository
    {
        User GetUserCalendars(int userId);
    }

    public class CalendarRepository : ICalendarRepository
    {
        private readonly TaskTideContext DbContext;

        public CalendarRepository(
            TaskTideContext taskTideContext)
        {
            this.DbContext = taskTideContext;
        }

        public User GetUserCalendars(int userId)
        {
            return
                this.DbContext
                    .Users
                    /* Calendars */
                    .Include(x => x.Calendars)
                        .ThenInclude(x => x.Color)
                    .Include(x => x.Calendars)
                        .ThenInclude(x => x.Invitations)
                    .Include(x => x.Calendars)
                        .ThenInclude(x => x.SharedWith)
                    .Include(x => x.Calendars)
                        .ThenInclude(x => x.TasksAndEvents.Where(y => !y.Deleted))
                        .ThenInclude(x => x.Recurrences.Where(y => !y.Deleted))
                    .Include(x => x.Calendars)
                        .ThenInclude(x => x.TasksAndEvents.Where(y => !y.Deleted))
                        .ThenInclude(x => x.Instances.Where(y => !y.Deleted))
                    /* Shared Calendars */
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.Color)
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.Invitations)
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.SharedWith)
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.TasksAndEvents.Where(y => !y.Deleted))
                        .ThenInclude(x => x.Recurrences.Where(y => !y.Deleted))
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.TasksAndEvents.Where(y => !y.Deleted))
                        .ThenInclude(x => x.Instances.Where(y => !y.Deleted))
                    /* Invitations */
                    .Include(x => x.InvitationsReceived.Where(y => !y.Deleted && y.RespondedAt == null))
                        .ThenInclude(x => x.Inviter)
                    .Include(x => x.InvitationsSent.Where(y => !y.Deleted && y.RespondedAt == null))
                        .ThenInclude(x => x.Invitee)
                    .AsSplitQuery()
                    .Single(x => x.Id == userId);
        }
    }
}
