using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExtractCore.Entity
{
    public partial class devcanContext : DbContext
    {
        public devcanContext()
        {
        }

        public devcanContext(DbContextOptions<devcanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<New> New { get; set; }
        public virtual DbSet<Old> Old { get; set; }

        // Unable to generate entity type for table 'dbo.old'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=RAKESHSWIN10\\RAKESHSAHU8;Database=devcan;User Id=sa;Password=mindfire");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<New>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("new");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastDownloadObuaId).HasColumnName("last_download_obua_id");

                entity.Property(e => e.LastDownloadTimestamp)
                    .HasColumnName("last_download_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasMaxLength(255);

                entity.Property(e => e.RecordCreateDate)
                    .HasColumnName("Record_Create_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UploadDate)
                    .HasColumnName("upload_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UploadObuaId).HasColumnName("upload_obua_id");
            });

            modelBuilder.Entity<Old>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("old");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastDownloadObuaId).HasColumnName("last_download_obua_id");

                entity.Property(e => e.LastDownloadTimestamp)
                    .HasColumnName("last_download_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasColumnType("image");

                entity.Property(e => e.RecordCreateDate)
                    .HasColumnName("Record_Create_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UploadDate)
                    .HasColumnName("upload_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UploadObuaId).HasColumnName("upload_obua_id");
            });
        }
    }
}
