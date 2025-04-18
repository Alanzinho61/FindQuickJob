using FindQuickJob.Domain.BaseClass;
using FindQuickJob.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<User> Users => Set<User>();
        public DbSet<JobPost> JopPosts => Set<JobPost>();
        public DbSet<Application> Applications => Set<Application>();

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<BaseEntity>().UseTptMappingStrategy();

        //    modelBuilder.Entity<JobPost>().ToTable("JobPosts");
        //    modelBuilder.Entity<User>().ToTable("Users");
        //    modelBuilder.Entity<Application>().ToTable("Applications");

        //    modelBuilder.Entity<BaseEntity>()
        //        .Property(b => b.Id)
        //        .HasDefaultValueSql("newsequentialid()") 
        //        .ValueGeneratedOnAdd(); // Burada Guidler icin otomatik deger atamasi yaptik


        //    modelBuilder.Entity<JobPost>()
        //        .HasOne(j => j.CreatedByUser)
        //        .WithMany(u => u.JopPosts)
        //        .HasForeignKey(j => j.CreatedByUserId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<Application>()
        //        .HasOne(a => a.User)
        //        .WithMany(u => u.Applications)
        //        .HasForeignKey(a => a.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<Application>()
        //        .HasOne(a => a.JopPost)
        //        .WithMany(j => j.Applications)
        //        .HasForeignKey(a => a.JopPostId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<JobPost>()
        //        .Property(j => j.JopType)
        //        .HasConversion<string>();
        //    modelBuilder.Entity<JobPost>()
        //        .Property(j => j.WorkType)
        //        .HasConversion<string>();
        //    modelBuilder.Entity<Application>()
        //        .Property(a => a.Status)
        //        .HasConversion<string>();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.Id)
                      .HasDefaultValueSql("newsequentialid()")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<JobPost>(entity =>
            {
                entity.ToTable("JobPosts");

                entity.Property(e => e.Id)
                      .HasDefaultValueSql("newsequentialid()")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .ValueGeneratedOnAddOrUpdate();

                entity.Property(j => j.WorkType).HasConversion<string>();
                entity.Property(j => j.JopType).HasConversion<string>();

                entity.HasOne(j => j.CreatedByUser)
                      .WithMany(u => u.JopPosts)
                      .HasForeignKey(j => j.CreatedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Applications");

                entity.Property(e => e.Id)
                      .HasDefaultValueSql("newsequentialid()")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .ValueGeneratedOnAddOrUpdate();

                entity.Property(a => a.Status).HasConversion<string>();

                entity.HasOne(a => a.User)
                      .WithMany(u => u.Applications)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.JopPost)
                      .WithMany(j => j.Applications)
                      .HasForeignKey(a => a.JopPostId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
