﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Viber.Models;

[Table("Moodboard")]
public partial class Moodboard
{
    [Key]
    [Column("Moodboard_Id")]
    public int MoodboardId { get; set; }

    [Column("User_Id")]
    public int UserId { get; set; }

    [Column("Date_Of_Creation", TypeName = "date")]
    public DateTime DateOfCreation { get; set; }

    [Column("Update_Date", TypeName = "date")]
    public DateTime? UpdateDate { get; set; }

    [Column("Background_Color")]
    [StringLength(7)]
    [Unicode(false)]
    public string BackgroundColor { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string Title { get; set; }

    [Column("Title_Color")]
    [StringLength(7)]
    [Unicode(false)]
    public string TitleColor { get; set; }

    [Column("PrimaryTag_Id")]
    public int PrimaryTagId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Thumbnail { get; set; }

    [InverseProperty("Moodboard")]
    public virtual ICollection<ContentContainer> ContentContainers { get; set; } = new List<ContentContainer>();

    [InverseProperty("Moodboard")]
    public virtual ICollection<MoodboardSubTag> MoodboardSubTags { get; set; } = new List<MoodboardSubTag>();

    [ForeignKey("PrimaryTagId")]
    [InverseProperty("Moodboards")]
    public virtual PrimaryTag PrimaryTag { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Moodboards")]
    public virtual User User { get; set; }
}