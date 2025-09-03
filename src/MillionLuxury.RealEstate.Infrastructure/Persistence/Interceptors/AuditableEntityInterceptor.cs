using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MillionLuxury.RealEstate.Domain.Entities.Interfaces;
using MillionLuxury.RealEstate.Infrastructure.Identity.Interfaces;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Interceptors;
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditableEntityInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var currentUser = _currentUserService.GetUserEmail();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is IAuditable auditableEntity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditableEntity.CreatedAt = utcNow;
                        auditableEntity.CreatedBy = currentUser;
                        break;

                    case EntityState.Modified:
                        auditableEntity.LastModifiedAt = utcNow;
                        auditableEntity.LastModifiedBy = currentUser;
                        entry.Property(nameof(IAuditable.CreatedAt)).IsModified = false;
                        entry.Property(nameof(IAuditable.CreatedBy)).IsModified = false;
                        break;
                }
            }

            if (entry.Entity is ISoftDeletable softDeletableEntity && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeletedAt = utcNow;
                softDeletableEntity.DeletedBy = currentUser;

                if (entry.Entity is IAuditable auditable)
                {
                    auditable.LastModifiedAt = utcNow;
                    auditable.LastModifiedBy = currentUser;
                }
            }
        }
    }
}
