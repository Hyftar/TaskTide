﻿using NodaTime;
using NodaTime.Extensions;
using TaskTideAPI.DataContexts;
using TaskTideAPI.Models;

namespace TaskTideAPI.Repositories
{
    public interface ITransactionEventsRepository
    {
        void Log<T>(T modelInstance, TransactionType transactionType, User? user = null, bool saveChanges = false) where T : ITransactionItem;
    }

    public class TransactionEventsRepository : ITransactionEventsRepository
    {
        private readonly TaskTideContext TaskTideContext;
        private readonly ZonedClock Clock;

        public TransactionEventsRepository(
            TaskTideContext taskTideContext,
            ZonedClock clock)
        {
            this.TaskTideContext = taskTideContext;
            this.Clock = clock;
        }

        public void Log<T>(T modelInstance, TransactionType transactionType, User? user = null, bool saveChanges = false) where T : ITransactionItem
        {
            this.TaskTideContext
                .TransactionnalEvents
                .Add(
                    new()
                    {
                        Model = modelInstance.GetType().Name,
                        ModelId = modelInstance.Id,
                        Timestamp = this.Clock.GetCurrentZonedDateTime(),
                        Type = transactionType,
                        User = user,
                    }
                );

            if (saveChanges)
            {
                this.TaskTideContext.SaveChanges();
            }
        }
    }
}
