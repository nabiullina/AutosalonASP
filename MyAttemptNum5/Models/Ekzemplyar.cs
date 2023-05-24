using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Ekzemplyar
{
    public string VinKod { get; set; } = null!;

    public decimal? IdA { get; set; }

    public decimal? IdD { get; set; }

    public virtual Automobile? IdANavigation { get; set; }

    public virtual Dogovor? IdDNavigation { get; set; }
}
