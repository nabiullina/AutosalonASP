using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Automobile
{
    public decimal IdA { get; set; }

    public string Model { get; set; } = null!;

    public DateTime? ReleaseDate { get; set; }

    public string Color { get; set; } = null!;

    public decimal KolVo { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Ekzemplyar> Ekzemplyars { get; set; } = new List<Ekzemplyar>();

    public virtual ICollection<Komplektaciya> IdKs { get; set; } = new List<Komplektaciya>();
}
