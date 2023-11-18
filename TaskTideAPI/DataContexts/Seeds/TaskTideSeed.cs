using Microsoft.EntityFrameworkCore;

namespace TaskTideAPI.DataContexts.Seeds
{
    public static class TaskTideSeed
    {
        public static void ApplyAllSeeds(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyColorSeed();
        }
    }
}
