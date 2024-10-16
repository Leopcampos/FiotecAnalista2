using System.ComponentModel.DataAnnotations;

namespace FiotecInfodengue.Application.Dtos;

public class RelatorioDto
{
    public int Geocode { get; set; }
    public string Disease { get; set; }
    public string Format { get; set; }
    [Range(1, 53)]
    public int EwStart { get; set; }
    [Range(1, 53)]
    public int EwEnd { get; set; }
    [Range(0, 9999)]
    public int EyStart { get; set; }
    [Range(0, 9999)]
    public int EyEnd { get; set; }
}