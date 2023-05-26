using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Komplektaciya
{
    public long IdK { get; set; }

    public string? NameK { get; set; }

    public string? Opisanie { get; set; }

    public long? Price { get; set; }

    public virtual ICollection<KomplektaciyaEkzemplyar> KomplektaciyaEkzemplyars { get; set; } = new List<KomplektaciyaEkzemplyar>();
}
