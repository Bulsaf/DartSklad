using System;
using System.Collections.Generic;

namespace DartSklad.Models;

public partial class Storage
{
    public Guid Id { get; set; }

    public Storage()
    {
        Id = Guid.NewGuid();
    }



    public string Title { get; set; } = null!;

    public Guid? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<SubjectsStorage> SubjectsStorages { get; set; } = new List<SubjectsStorage>();
}
