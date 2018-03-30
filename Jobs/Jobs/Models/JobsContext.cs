using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Jobs.Models
{
    public partial class JobsContext : DbContext
    {
        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Cv> Cv { get; set; }
        public virtual DbSet<Interests> Interests { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersInterests> UsersInterests { get; set; }


        public JobsContext(DbContextOptions<JobsContext> options)
    : base(options)
        { }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(local);Database=Jobs;Integrated Security=True;Trusted_Connection=True;");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applications>(entity =>
            {
                entity.HasKey(e => e.ApplicationId);

                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Applications_Jobs");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Applications_Users");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Email).HasColumnType("nchar(300)");

                entity.Property(e => e.Location).HasColumnType("nchar(300)");
            });

            modelBuilder.Entity<Cv>(entity =>
            {
                entity.ToTable("CV");

                entity.Property(e => e.CvId).HasColumnName("CV_ID");

                entity.Property(e => e.CvPath).HasColumnName("CV_path");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cv)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CV_Users");
            });

            modelBuilder.Entity<Interests>(entity =>
            {
                entity.HasKey(e => e.InterestId);

                entity.Property(e => e.InterestId).HasColumnName("Interest_ID");

                entity.Property(e => e.Name).HasColumnType("nchar(200)");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.JobId)
                    .HasColumnName("Job_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Type).HasColumnType("nchar(50)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Jobs_Companies");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.Email).HasColumnType("nchar(255)");

                entity.Property(e => e.FirstName).HasColumnType("nchar(255)");

                entity.Property(e => e.LastName).HasColumnType("nchar(255)");

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.Photo).HasMaxLength(200);
            });

            modelBuilder.Entity<UsersInterests>(entity =>
            {
                entity.ToTable("Users_interests");

                entity.Property(e => e.UsersInterestsId).HasColumnName("Users_interests_ID");

                entity.Property(e => e.InterestId).HasColumnName("Interest_ID");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.UsersInterests)
                    .HasForeignKey(d => d.InterestId)
                    .HasConstraintName("FK_Users_interests_Interests");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.UsersInterests)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Users_interests_Jobs");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersInterests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Users_interests_Users");
            });
        }
    }
}
