using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team0205.Models;

public partial class TaskModel
{
    [Key]
    [Required]
    public int TaskId { get; set; }
    public string? TaskName { get; set; }
    public string? DueDate { get; set; }
    public int Quadrant { get; set; }
    [ForeignKey("CategoryID")]
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public bool CompletedTask { get; set; }

}
