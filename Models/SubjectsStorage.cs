using System;
using System.Collections.Generic;

namespace DartSklad.Models;

public partial class SubjectsStorage
{
    public Guid Id { get; set; }

    public SubjectsStorage()
    {
        Id = Guid.NewGuid();
    }



    public Guid? SubjectId { get; set; }

    public Guid? StorageId { get; set; }

    public virtual Storage? Storage { get; set; }

    public virtual Subject? Subject { get; set; }
}
