using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Dogovor
{
    public long IdD { get; set; }

    public DateTime DateOfExecution { get; set; }

    public long? IdB { get; set; }
    
    public string VinKod { get; set; }

    public virtual Ekzemplyar Ekzemplyar { get; set; }

    public virtual Buyer? IdBNavigation { get; set; }
}
