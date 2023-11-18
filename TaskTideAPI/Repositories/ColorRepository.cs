using TaskTideAPI.DataContexts;
using TaskTideAPI.Models;

namespace TaskTideAPI.Repositories
{
    public interface IColorRepository
    {
        TaskEventColor GetColorByName(string name);
    }
    public class ColorRepository : IColorRepository
    {
        private readonly TaskTideContext DataContext;

        public ColorRepository(
            TaskTideContext taskTideContext)
        {
            this.DataContext = taskTideContext;
        }

        public TaskEventColor GetColorByName(string name)
        {
            return this.DataContext.TaskEventColors.Single(x => x.Name == name);
        }
    }
}
