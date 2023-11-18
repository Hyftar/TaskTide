using Microsoft.EntityFrameworkCore;
using TaskTideAPI.Models;

namespace TaskTideAPI.DataContexts.Seeds
{
    public static class ColorSeed
    {
        public static void ApplyColorSeed(this ModelBuilder modelBuilder)
        {
            var colors =
                new[]
                {
                    new TaskEventColor
                    {
                        Id = 1,
                        Name = "Default",
                        Red = 92,
                        Green = 128,
                        Blue = 203,
                    },
                    new TaskEventColor
                    {
                        Id = 2,
                        Name = "Orange",
                        Red = 255,
                        Green = 150,
                        Blue = 79,
                    },
                    new TaskEventColor
                    {
                        Id = 3,
                        Name = "Red",
                        Red = 255,
                        Green = 105,
                        Blue = 97,
                    },
                    new TaskEventColor
                    {
                        Id = 4,
                        Name = "Lavender",
                        Red = 174,
                        Green = 122,
                        Blue = 204,
                    },
                };

            modelBuilder.Entity<TaskEventColor>().HasData(colors);
        }
    }
}
