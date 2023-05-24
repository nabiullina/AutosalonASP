using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Dogovor
{
    public decimal IdD { get; set; }

    public DateTime DateOfExecution { get; set; }

    public decimal? IdB { get; set; }

    public decimal? IdM { get; set; }

    public virtual ICollection<Ekzemplyar> Ekzemplyars { get; set; } = new List<Ekzemplyar>();

    public virtual Buyer? IdBNavigation { get; set; }

    public virtual Manager? IdMNavigation { get; set; }
}
