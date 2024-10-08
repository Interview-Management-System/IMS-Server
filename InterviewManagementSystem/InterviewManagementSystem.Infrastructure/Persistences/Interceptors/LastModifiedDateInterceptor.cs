using InterviewManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InterviewManagementSystem.Infrastructure.Persistences.Interceptors
{
    internal sealed class LastModifiedDateInterceptor : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            return base.SavedChanges(eventData, result);
        }

        private void SetLastModifiedDate(DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity);

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                entity.UpdateAt = DateTime.Now; // or DateTime.Now
            }
        }
    }
}
