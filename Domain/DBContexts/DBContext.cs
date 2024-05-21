using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topt_like_asp_webapi.Domain.Entities;
using Topt_like_asp_webapi.Domain.Entities.Base;

namespace Topt_like_asp_webapi.Domain.DBContexts
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Tab> Tabs { get; set; }


        // public override int SaveChanges()
        // {
        //     UpdateUpdatedProperty<User>();
        //     UpdateUpdatedProperty<Space>();
        //     UpdateUpdatedProperty<Collection>();
        //     UpdateUpdatedProperty<Tab>();
        //     return base.SaveChanges();
        // }

        // public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        // {
        //     UpdateUpdatedProperty<User>();
        //     UpdateUpdatedProperty<Space>();
        //     UpdateUpdatedProperty<Collection>();
        //     UpdateUpdatedProperty<Tab>();
        //     return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        // }

        // private void UpdateUpdatedProperty<T>() where T : BaseEntity
        // {
        //     var modifiedSourceInfo =
        //         ChangeTracker.Entries<T>()
        //             .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        //     foreach (var entry in modifiedSourceInfo)
        //     {
        //         entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        //     }
        // }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     // user
        //     modelBuilder.Entity<User>()
        //         .HasMany(a => a.Spaces)
        //         .WithOne(i => i.User)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<User>()
        //         .HasMany(a => a.Collections)
        //         .WithOne(i => i.User)
        //         .OnDelete(DeleteBehavior.Cascade)
        //         ;

        //     modelBuilder.Entity<User>()
        //         .HasMany(a => a.Tabs)
        //         .WithOne(i => i.User)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<Space>()
        //         .HasOne(a => a.User)
        //         .WithMany(i => i.Spaces)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<Collection>()
        //         .HasOne(a => a.User)
        //         .WithMany(i => i.Collections)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<Tab>()
        //         .HasOne(a => a.User)
        //         .WithMany(i => i.Tabs)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     // space
        //     modelBuilder.Entity<Space>()
        //          .HasMany(a => a.Collections)
        //          .WithOne(i => i.Space)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<Collection>()
        //         .HasOne(a => a.Space)
        //         .WithMany(i => i.Collections)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     // collction
        //     modelBuilder.Entity<Collection>()
        //          .HasMany(a => a.Tabs)
        //          .WithOne(i => i.Collection)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<Tab>()
        //         .HasOne(a => a.Collection)
        //         .WithMany(i => i.Tabs)
        //         .OnDelete(DeleteBehavior.Cascade);

        // }

    }
}
