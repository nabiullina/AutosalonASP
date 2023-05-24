using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Manager
{
    public decimal IdM { get; set; }

    public string Fio { get; set; } = null!;

    public decimal? Age { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Dogovor> Dogovors { get; set; } = new List<Dogovor>();
}
