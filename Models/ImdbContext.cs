using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Mandatory_IMDB_Assignment.Models;

public partial class ImdbContext : DbContext
{
    public ImdbContext()
    {
    }

    public ImdbContext(DbContextOptions<ImdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenresAndTitlesPublic> GenresAndTitlesPublics { get; set; }

    public virtual DbSet<KnownForTitle> KnownForTitles { get; set; }

    public virtual DbSet<NameBasic> NameBasics { get; set; }

    public virtual DbSet<PrimaryProfession> PrimaryProfessions { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<TitleCrew> TitleCrews { get; set; }

    public virtual DbSet<TitleDirector> TitleDirectors { get; set; }

    public virtual DbSet<TitleWriter> TitleWriters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost;database=IMDB; user id=sa;password=fai789;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK_Genres_1");

            entity.HasIndex(e => e.GenreName, "genre_idx");

            entity.HasIndex(e => e.GenreName, "idx_genere_name");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.GenreName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("genre_name");
            entity.Property(e => e.Tconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tconst");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.Genres)
                .HasForeignKey(d => d.Tconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tconst_fk");
        });

        modelBuilder.Entity<GenresAndTitlesPublic>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GenresAndTitlesPublic");

            entity.Property(e => e.GenreName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("genre_name");
            entity.Property(e => e.IsAdult).HasColumnName("isAdult");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("originalTitle");
            entity.Property(e => e.PrimaryTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("primaryTitle");
            entity.Property(e => e.RuntimeMinutes).HasColumnName("runtimeMinutes");
            entity.Property(e => e.TitleType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titleType");
        });

        modelBuilder.Entity<KnownForTitle>(entity =>
        {
            entity.HasKey(e => e.KnownId);

            entity.ToTable("known_for_titles");

            entity.Property(e => e.KnownId).HasColumnName("knownId");
            entity.Property(e => e.Nconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nconst");
            entity.Property(e => e.TitleId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title_id");

            entity.HasOne(d => d.NconstNavigation).WithMany(p => p.KnownForTitles)
                .HasForeignKey(d => d.Nconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_title_names_known_for_nconst");
        });

        modelBuilder.Entity<NameBasic>(entity =>
        {
            entity.HasKey(e => e.Nconst).HasName("PK__Title_na__49B947A5430A48E4");

            entity.ToTable("Name_basic");

            entity.HasIndex(e => e.PrimaryName, "staff_name_idx");

            entity.Property(e => e.Nconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nconst");
            entity.Property(e => e.BirthYear).HasColumnName("birthYear");
            entity.Property(e => e.DeathYear).HasColumnName("deathYear");
            entity.Property(e => e.PrimaryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primaryName");
        });

        modelBuilder.Entity<PrimaryProfession>(entity =>
        {
            entity.HasKey(e => e.ProfId).HasName("PK__primary___49B947A5B93BA031");

            entity.ToTable("primary_profession");

            entity.Property(e => e.Nconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nconst");
            entity.Property(e => e.Profession)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profession");

            entity.HasOne(d => d.NconstNavigation).WithMany(p => p.PrimaryProfessions)
                .HasForeignKey(d => d.Nconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_title_name_nconst");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Staff");

            entity.Property(e => e.BirthYear).HasColumnName("birthYear");
            entity.Property(e => e.DeathYear).HasColumnName("deathYear");
            entity.Property(e => e.PrimaryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primaryName");
            
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.Tconst).HasName("PK__titles__85FD5344805D2A92");

            entity.HasIndex(e => new { e.PrimaryTitle, e.TitleType, e.IsAdult }, "title_idx");

            entity.Property(e => e.Tconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tconst");
            entity.Property(e => e.EndYear).HasColumnName("endYear");
            entity.Property(e => e.IsAdult).HasColumnName("isAdult");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("originalTitle");
            entity.Property(e => e.PrimaryTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("primaryTitle");
            entity.Property(e => e.RuntimeMinutes).HasColumnName("runtimeMinutes");
            entity.Property(e => e.StartYear).HasColumnName("startYear");
            entity.Property(e => e.TitleType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titleType");
        });

        modelBuilder.Entity<TitleCrew>(entity =>
        {
            entity.HasKey(e => e.Tconst).HasName("PK__title_cr__85FD534480577E75");

            entity.ToTable("title_crew");

            entity.Property(e => e.Tconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tconst");
        });

        modelBuilder.Entity<TitleDirector>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__title_di__85FD534489D4E187");

            entity.ToTable("title_directors");

            entity.Property(e => e.DirectorId).HasColumnName("directorId");
            entity.Property(e => e.DirectorNconst)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("director_nconst");
            entity.Property(e => e.Tconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tconst");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.TitleDirectors)
                .HasForeignKey(d => d.Tconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_title_directors_title_crew");
        });

        modelBuilder.Entity<TitleWriter>(entity =>
        {
            entity.HasKey(e => e.WriterId);

            entity.ToTable("title_writers");

            entity.Property(e => e.WriterId).HasColumnName("writerId");
            entity.Property(e => e.Tconst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tconst");
            entity.Property(e => e.WriterNconst)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("writer_nconst");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.TitleWriters)
                .HasForeignKey(d => d.Tconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_title_writers_title_writers_tconst");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
