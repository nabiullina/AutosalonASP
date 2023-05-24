using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Buyer
{
    public decimal IdB { get; set; }

    public string Fio { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Dogovor> Dogovors { get; set; } = new List<Dogovor>();
}
