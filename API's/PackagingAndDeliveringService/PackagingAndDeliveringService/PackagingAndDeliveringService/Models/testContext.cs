using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PackagingAndDeliveringService.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoginDetail> LoginDetails { get; set; }
        public virtual DbSet<ProcessDetail> ProcessDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=test;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<LoginDetail>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__LoginDet__536C85E551343023");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProcessDetail>(entity =>
            {
                entity.HasKey(e => e.ContactNumber)
                    .HasName("PK__ProcessD__570665C7A0BD8D0F");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DateofDelivery).HasColumnType("date");

                entity.Property(e => e.IsPriorityRequest)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.NameNavigation)
                    .WithMany(p => p.ProcessDetails)
                    .HasForeignKey(d => d.Name)
                    .HasConstraintName("FK__ProcessDet__Name__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
