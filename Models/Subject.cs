using System;
using System.Collections.Generic;

namespace DartSklad.Models;

public partial class Subject
{
    public Guid Id { get; set; }

    public Subject()
    {
        Id = Guid.NewGuid();
    }



    public string? SerialNumber { get; set; }

    public string Title { get; set; } = null!;

    public int? Price { get; set; }

    public Guid? SubjectTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual SubjectType? SubjectType { get; set; }

    public virtual ICollection<SubjectsStorage> SubjectsStorages { get; set; } = new List<SubjectsStorage>();
}
