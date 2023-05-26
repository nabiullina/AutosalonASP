using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Automobile
{
    public long IdA { get; set; }

    public string Model { get; set; } = null!;

    public DateTime? ReleaseDate { get; set; }

    public string Color { get; set; } = null!;

    public long KolVo { get; set; }

    public long Price { get; set; }

    public virtual ICollection<Ekzemplyar> Ekzemplyars { get; set; } = new List<Ekzemplyar>();

}
