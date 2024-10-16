namespace FiotecInfodengue.Domain.Entities;

public class RelatorioDto
{
    public int Geocode { get; set; }
    public string Disease { get; set; }
    public string Format { get; set; }
    public int EwStart { get; set; }
    public int EwEnd { get; set; }
    public int EyStart { get; set; }
    public int EyEnd { get; set; }
}