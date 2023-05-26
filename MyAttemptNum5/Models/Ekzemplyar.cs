using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Ekzemplyar
{
    public string VinKod { get; set; } = null!;

    public long? IdA { get; set; }

    public long? IdD { get; set; }

    public virtual Automobile? IdANavigation { get; set; }

    public virtual Dogovor? IdDNavigation { get; set; }
    
    public virtual ICollection<KomplektaciyaEkzemplyar> KomplektaciyaEkzemplyars { get; set; } = new List<KomplektaciyaEkzemplyar>();

}
