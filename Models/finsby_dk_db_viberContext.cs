﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Viber.Models;

public partial class finsby_dk_db_viberContext : DbContext
{

    public finsby_dk_db_viberContext(DbContextOptions<finsby_dk_db_viberContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContentContainer> ContentContainers { get; set; }

    public virtual DbSet<Moodboard> Moodboards { get; set; }

    public virtual DbSet<MoodboardSubTag> MoodboardSubTags { get; set; }

    public virtual DbSet<PrimaryTag> PrimaryTags { get; set; }

    public virtual DbSet<SubTag> SubTags { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContentContainer>(entity =>
        {
            entity.HasKey(e => e.ContentContainterId).HasName("PK__ContentC__5AB3C8A78F7FA029");

            entity.HasOne(d => d.Moodboard).WithMany(p => p.ContentContainers).HasConstraintName("FK__ContentCo__Moodb__46E78A0C");
        });

        modelBuilder.Entity<Moodboard>(entity =>
        {
            entity.HasKey(e => e.MoodboardId).HasName("PK__Moodboar__A52F4D9F252D6F0F");

            entity.HasOne(d => d.PrimaryTag).WithMany(p => p.Moodboards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Moodboard__Prima__3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.Moodboards).HasConstraintName("FK__Moodboard__User___398D8EEE");
        });

        modelBuilder.Entity<MoodboardSubTag>(entity =>
        {
            entity.HasKey(e => e.MoodboardSubtagId).HasName("PK__Moodboar__A401C6EF7E164C94");

            entity.HasOne(d => d.Moodboard).WithMany(p => p.MoodboardSubTags).HasConstraintName("FK__Moodboard__Moodb__4316F928");

            entity.HasOne(d => d.Subtag).WithMany(p => p.MoodboardSubTags).HasConstraintName("FK__Moodboard__Subta__440B1D61");
        });

        modelBuilder.Entity<PrimaryTag>(entity =>
        {
            entity.HasKey(e => e.PrimaryTagId).HasName("PK__PrimaryT__96B48A2846F32424");
        });

        modelBuilder.Entity<SubTag>(entity =>
        {
            entity.HasKey(e => e.SubTagId).HasName("PK__SubTag__D1DD20068BB69F0A");

            entity.HasOne(d => d.PrimaryTag).WithMany(p => p.SubTags)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubTag__PrimaryT__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__206D9170D9458D45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}