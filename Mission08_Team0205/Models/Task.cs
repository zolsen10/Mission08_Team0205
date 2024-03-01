using System;
using System.Collections.Generic;

namespace Mission08_Team0205.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string? TaskName { get; set; }

    public string? DueDate { get; set; }

    public int Quadrant { get; set; }

    public int? CategoryId { get; set; }

    public int? Completed { get; set; }

    public Category? Category { get; set; }
}
