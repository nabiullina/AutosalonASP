using System;
using System.Collections.Generic;

namespace MyAttemptNum5.Models;

public partial class Buyer
{
    public long IdB { get; set; }

    
    public string Fio { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Dogovor> Dogovors { get; set; } = new List<Dogovor>();
    
    public string Email { get; set; }
    public string Password { get; set; }
}
