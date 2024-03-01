using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0205.Models;

public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}
