using System;
using System.Collections.Generic;

namespace DartSklad.Models;

public partial class SubjectType
{

    public SubjectType()
    {
        Id = Guid.NewGuid();
    }


    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
