namespace MyAttemptNum5.Models;

public class KomplektaciyaEkzemplyar
{
    public long IdK { get; set; }
    public string IdE { get; set; }
    public virtual Komplektaciya Komplektaciya { get; set; }
    public virtual Ekzemplyar Ekzemplyar { get; set; }
}