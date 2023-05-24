using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Komplektaciya
{
    public decimal IdK { get; set; }

    public string? NameK { get; set; }

    public string? Opisanie { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Automobile> IdAs { get; set; } = new List<Automobile>();
}
