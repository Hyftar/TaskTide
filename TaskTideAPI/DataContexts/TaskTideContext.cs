using Microsoft.EntityFrameworkCore;
using TaskTideAPI.Models;

namespace TaskTideAPI.DataContexts;

public partial class TaskTideContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Calendar> Calendars { get; set; }

    public DbSet<CalendarInvitation> CalendarInvitations { get; set; }

    public DbSet<TaskEvent> TaskEvents { get; set; }

    public DbSet<Recurrence> Recurrences { get; set; }

    public DbSet<TaskEventInstance> TaskEventsInstance { get; set; }

    public DbSet<TaskEventColor> TaskEventColors { get; set; }

    public DbSet<TransactionnalEvents> TransactionnalEvents { get; set; }

    public TaskTideContext()
    {
    }

    public TaskTideContext(DbContextOptions<TaskTideContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:TaskTidePGLocal", x => x.UseNodaTime());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        this.OnModelCreatingPartial(modelBuilder);

        modelBuilder
            .Entity<Calendar>()
            .HasMany(x => x.SharedWith)
            .WithMany(x => x.SharedCalendars);

        modelBuilder
            .Entity<Calendar>()
            .HasOne(x => x.Owner)
            .WithMany(x => x.Calendars);

        modelBuilder
            .Entity<CalendarInvitation>()
            .HasOne(x => x.Inviter)
            .WithMany(x => x.InvitationsSent);

        modelBuilder
            .Entity<CalendarInvitation>()
            .HasOne(x => x.Invitee)
            .WithMany(x => x.InvitationsReceived);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
