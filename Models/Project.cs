using System;
using System.Collections.Generic;

namespace DartSklad.Models;

public partial class Project
{
    public Guid Id { get; private set; }

    public Project()
    {
        Id = Guid.NewGuid();
    }

    public string Title { get; set; } = null!;

    public DateTime? EntryDate { get; set; }

    public DateTime? BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
