﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Viber.Models;

[Table("User")]
public partial class User
{
    [Key]
    [Column("User_Id")]
    public int UserId { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; }

    [Required]
    [StringLength(128)]
    [Unicode(false)]
    public string Password { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Moodboard> Moodboards { get; set; } = new List<Moodboard>();
}