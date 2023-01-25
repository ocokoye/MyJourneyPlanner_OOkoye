using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyJourneyPlanner_OOkoye.Models;

public partial class JourneyplannerdbContext : DbContext
{
    public JourneyplannerdbContext()
    {
    }

    public JourneyplannerdbContext(DbContextOptions<JourneyplannerdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Line> Lines { get; set; }

    public virtual DbSet<LineStation> LineStations { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<StationPair> StationPairs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-JV4K3QV; Database=journeyplannerdb; Encrypt=False;TrustServerCertificate=False; user id=sa; pwd=Pa$$w0rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Line>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Line__2EAE65292D6AC152");

            entity.ToTable("Line");

            entity.Property(e => e.LineColor).HasMaxLength(50);
            entity.Property(e => e.LineName).HasMaxLength(50);
        });

        modelBuilder.Entity<LineStation>(entity =>
        {
            entity.HasKey(e => e.LineStationId).HasName("PK__LineStat__B6CDD488C6F29533");

            entity.ToTable("LineStation");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Station__E0D8A6BDD62186CB");

            entity.ToTable("Station");

            entity.Property(e => e.StationName).HasMaxLength(50);
        });

        modelBuilder.Entity<StationPair>(entity =>
        {
            entity.HasKey(e => e.StationPairId).HasName("PK__StationP__1CAAE009ACF3F6CA");

            entity.ToTable("StationPair");

            entity.Property(e => e.Distance).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.TimeSpent).HasColumnType("decimal(8, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
