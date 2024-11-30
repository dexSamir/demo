using System;
using System.Collections.Generic;

namespace ToDoApp.Models;

public partial class Todo
{
    public int Id { get; set; }

    public DateOnly? Deadline { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }
}
