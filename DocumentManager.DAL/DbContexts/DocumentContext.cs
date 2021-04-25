using DocumentManager.Infrastructure;
using DocumentManager.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DocumentManager.DAL.Model
{
    public partial class DocumentContext : DbContext
    {
        private readonly IUserService _userService;

        private readonly ITenantService _tenantService;

        private readonly INotification _notification;

        public DocumentContext(
            DbContextOptions<DocumentContext> options,
            IUserService userService,
            ITenantService tenantService,
            INotification notification) : base(options)
        {
            _userService = userService;
            _tenantService = tenantService;
            _notification = notification;
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder)
        {
            foreach (var entityModelType in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = entityModelType.ClrType;

                modelBuilder.Entity(entityType).Property<DateTime>(@"CreatedOn").HasColumnName(@"CreatedOn").IsRequired().ValueGeneratedNever();
                modelBuilder.Entity(entityType).Property<string>(@"CreatedBy").HasColumnName(@"CreatedBy").IsRequired().ValueGeneratedNever();
                modelBuilder.Entity(entityType).Property<DateTime?>(@"ModifiedOn").HasColumnName(@"ModifiedOn").ValueGeneratedNever();
                modelBuilder.Entity(entityType).Property<string>(@"ModifiedBy").HasColumnName(@"ModifiedBy").ValueGeneratedNever();
                modelBuilder.Entity(entityType).Property<DateTime?>(@"DeletedOn").HasColumnName(@"DeletedOn").ValueGeneratedNever();
                modelBuilder.Entity(entityType).Property<string>(@"DeletedBy").HasColumnName(@"DeletedBy").ValueGeneratedNever();
                modelBuilder.Entity(entityType).Property<bool>(@"Deleted").HasColumnName(@"Deleted").ValueGeneratedNever();

                modelBuilder.Entity(entityType).Property<byte[]>("RowVersion")
                    .IsRequired()
                    .IsRowVersion();

                modelBuilder.Entity(entityType).Property<long>(@"TenantID").HasColumnName(@"TenantID").IsRequired().ValueGeneratedNever();

                //Expression<Func<bool>> deleted = () => false;
                //var parameter = Expression.Parameter(entityType, "e");
                //var bodyDeleted = Expression.Equal(
                //    left: Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("Deleted")),
                //    right: deleted.Body);


                //Expression<Func<long>> tenantID = () => 2 ;
                //var tenantParam = Expression.Parameter(entityType, "e");
                //var bodyTenant = Expression.Equal(
                //    left: Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(long) }, tenantParam, Expression.Constant("TenantID")),
                //    right: tenantID.Body);

                //var fullBody = Expression.And(bodyDeleted, bodyTenant);

                //modelBuilder.Entity(entityType).HasQueryFilter(Expression.Lambda(fullBody, parameter));
            }

            modelBuilder.Entity<Document>()
                .HasQueryFilter(b =>
                    EF.Property<long>(b, "TenantID") == _tenantService.TenantID &&
                    !EF.Property<bool>(b, "Deleted"));

            modelBuilder.Entity<DocumentType>()
                .HasQueryFilter(b =>
                    EF.Property<long>(b, "TenantID") == _tenantService.TenantID &&
                    !EF.Property<bool>(b, "Deleted"));

            modelBuilder.Entity<DocumentTypeGroup>()
                .HasQueryFilter(b =>
                    EF.Property<long>(b, "TenantID") == _tenantService.TenantID &&
                    !EF.Property<bool>(b, "Deleted"));

            modelBuilder.Entity<DocumentApplication>()
               .HasQueryFilter(b =>
                   EF.Property<long>(b, "TenantID") == _tenantService.TenantID &&
                   !EF.Property<bool>(b, "Deleted"));
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (item.Property(AuditingColumn.CreatedOn.ToString()) != null)
                {
                    item.Property(AuditingColumn.CreatedOn.ToString()).CurrentValue = DateTime.Now;
                }

                if (item.Property(AuditingColumn.CreatedBy.ToString()) != null)
                {
                    string currentUserUsername = _userService.Username;

                    if (string.IsNullOrWhiteSpace(currentUserUsername))
                    {
                        throw new Exception("The username for CreatedBy cannot be null, empty or whitespace");
                    }

                    item.Property(AuditingColumn.CreatedBy.ToString()).CurrentValue = currentUserUsername;
                }

                if (item.Property("TenantID") != null)
                {
                    item.Property("TenantID").CurrentValue = _tenantService.TenantID;
                }
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            {
                if (item.Property(AuditingColumn.ModifiedOn.ToString()) != null)
                {
                    item.Property(AuditingColumn.ModifiedOn.ToString()).CurrentValue = DateTime.Now;
                }

                if (item.Property(AuditingColumn.ModifiedBy.ToString()) != null)
                {
                    string currentUserUsername = _userService.Username;

                    if (string.IsNullOrWhiteSpace(currentUserUsername))
                    {
                        throw new Exception("The username for ModifiedBy cannot be null, empty or whitespace");
                    }

                    item.Property(AuditingColumn.ModifiedBy.ToString()).CurrentValue = currentUserUsername;
                }
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (item != null)
                {
                    if (item.Property(AuditingColumn.DeletedOn.ToString()) != null)
                    {
                        item.Property(AuditingColumn.DeletedOn.ToString()).CurrentValue = DateTime.Now;
                    }

                    if (item.Property(AuditingColumn.DeletedBy.ToString()) != null)
                    {
                        string currentUserUsername = _userService.Username;

                        if (string.IsNullOrWhiteSpace(currentUserUsername))
                        {
                            throw new Exception("The username for DeletedBy cannot be null, empty or whitespace");
                        }

                        item.Property(AuditingColumn.DeletedBy.ToString()).CurrentValue = currentUserUsername;
                    }
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    var proposedValues = entry.CurrentValues;
                    var databaseValues = entry.GetDatabaseValues();

                    foreach (var property in proposedValues.Properties)
                    {
                        var proposedValue = proposedValues[property];
                        var databaseValue = databaseValues[property];

                        proposedValues[property] = databaseValue;
                    }

                    // Refresh original values to bypass next concurrency check
                    entry.OriginalValues.SetValues(databaseValues);
                }

                _notification.AddGeneralError("Database concurrency exception occurred", ExceptionType.ConcurrencyException, ex);
            }

            return -1;
        }

        private enum AuditingColumn
        {
            CreatedOn,
            CreatedBy,
            ModifiedOn,
            ModifiedBy,
            DeletedOn,
            DeletedBy
        }
    }
}
