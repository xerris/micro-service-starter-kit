using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skillz.Services.Context.Models;
using Skillz.Services.Extensions;
using Xerris.DotNet.Core.Time;

namespace Skillz.Services.Context;

public class DataContext: DbContext
{
    private int? TokenUserId { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DataContext WithUserId(int userId)
    {
        TokenUserId = userId;
        return this;
    }

    //public DbSet<Rope> Ropes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("hoist");

        //modelBuilder.ApplyConfiguration(new SiteConfig());

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeConverter>();

        base.ConfigureConventions(configurationBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: IAuditable, State: EntityState.Added });

        foreach (var entry in entries)
        {
            if(entry.Entity is not IAuditable entityEntry) continue;

            if (TokenUserId.HasValue)
            {
                entityEntry.ModifiedBy = TokenUserId;
                entityEntry.CreatedBy ??= TokenUserId.Value;
            }

            entityEntry.CreatedOn = Clock.Utc.Now;
            entityEntry.ModifiedOn = Clock.Utc.Now;
            entityEntry.SynchronizedOn = Clock.Utc.Now;
        }

        var modifiedEntries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: IAuditable, State: EntityState.Modified });

        foreach (var entry in modifiedEntries)
        {
            if(entry.Entity is not IAuditable entityEntry) continue;

            entityEntry.ModifiedOn = Clock.Utc.Now;
            entityEntry.SynchronizedOn = Clock.Utc.Now;

            if (TokenUserId.HasValue)
                entityEntry.ModifiedBy = TokenUserId.Value;
        }

        //deleted entities
        var deletedEntries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: IDeleteable, State: EntityState.Deleted });

        foreach (var entry in deletedEntries)
        {
            ((IDeleteable) entry.Entity).IsDeleted = true;
            entry.State = EntityState.Modified;

            if(entry.Entity is not IAuditable auditableEntry) continue;

            auditableEntry.ModifiedOn = Clock.Utc.Now;
            auditableEntry.SynchronizedOn = Clock.Utc.Now;

            if (TokenUserId.HasValue)
                auditableEntry.ModifiedBy = TokenUserId.Value;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

public class DateTimeConverter() : ValueConverter<DateTime, DateTime>(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

public class EnumToDescriptionConverter<TEnum>() : 
    ValueConverter<TEnum, string>(v => v.GetDescription(), v => v.FromDescription<TEnum>()) where TEnum : Enum;