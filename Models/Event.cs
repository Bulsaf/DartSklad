using System;
using System.Collections.Generic;

namespace DartSklad.Models;

public partial class Event
{
    public Guid Id { get; set; }

    public Event()
    {
        Id = Guid.NewGuid();
    }



    public string Title { get; set; } = null!;

    public string? ServiceTitle { get; set; }

    public DateTime? EntryDate { get; set; }

    public DateTime? BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? ProjectId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();
}
