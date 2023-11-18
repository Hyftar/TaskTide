using Microsoft.EntityFrameworkCore;
using TaskTideAPI.DataContexts;
using TaskTideAPI.Models;

namespace TaskTideAPI.Repositories
{
    public interface ICalendarRepository
    {
        Calendar? GetCalendar(int userId, int calendarId);

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

        public Calendar? GetCalendar(int userId, int calendarId)
        {
            return
                this.DbContext
                    .Calendars
                    .Where(x => x.Owner.Id == userId)
                    .Include(x => x.Color)
                    .Include(x => x.Invitations.Where(y => !y.Deleted && y.RespondedAt == null))
                    .Include(x => x.SharedWith)
                    .Include(x => x.TasksAndEvents.Where(y => !y.Deleted))
                        .ThenInclude(x => x.Instances)
                    .Include(x => x.TasksAndEvents.Where(y => !y.Deleted))
                        .ThenInclude(x => x.Recurrences)
                    .Include(x => x.TasksAndEvents.Where(y => !y.Deleted))
                        .ThenInclude(x => x.LunarCalendarRecurrences)
                    .AsSplitQuery()
                    .FirstOrDefault();
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
                    /* Shared Calendars */
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.Color)
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.Invitations)
                    .Include(x => x.SharedCalendars)
                        .ThenInclude(x => x.SharedWith)
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
