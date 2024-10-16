using FiotecInfodengue.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class RelatoriosController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public RelatoriosController(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    /// <summary>
    /// Listar todos os dados epidemiológicos dos municípios.
    /// </summary>
    [HttpGet("listar-todos-por-municipio")]
    public async Task<IActionResult> GetTodosRelatorios([FromQuery] RelatorioDto dto)
    {
        return await RealizarConsultaApi(dto);
    }

    /// <summary>
    /// Listar os dados epidemiológicos dos municípios pelo código IBGE.
    /// </summary>
    [HttpGet("listar-por-codigo-ibge")]
    public async Task<IActionResult> GetRelatorioPorCodigoIbge([FromQuery] int geocode)
    {
        var dto = new RelatorioDto
        {
            Geocode = geocode
        };

        return await RealizarConsultaApi(dto);
    }

    /// <summary>
    /// Listar os dados epidemiológicos dos municípios pelo código IBGE, semana início, semana fim e arbovirose.
    /// </summary>
    [HttpGet("listar-por-ibge-semana-arbovirose")]
    public async Task<IActionResult> GetRelatorioPorIbgeSemanaArbovirose([FromQuery] RelatorioDto dto)
    {
        return await RealizarConsultaApi(dto);
    }

    /// <summary>
    /// Listar a quantidade máxima e mínima estimadas de casos epidemiológicos dos municípios por semana.
    /// </summary>
    [HttpGet("listar-max-min-casos-por-semana")]
    public async Task<IActionResult> GetMaxMinCasos([FromQuery] RelatorioDto dto)
    {
        return await RealizarConsultaApi(dto);
    }

    /// <summary>
    /// Listar o total de casos epidemiológicos dos municípios.
    /// </summary>
    [HttpGet("listar-total-casos")]
    public async Task<IActionResult> GetTotalCasos([FromQuery] RelatorioDto dto)
    {
        return await RealizarConsultaApi(dto);
    }

    /// <summary>
    /// Listar o total de casos epidemiológicos dos municípios por arbovirose.
    /// </summary>
    [HttpGet("listar-total-casos-por-arbovirose")]
    public async Task<IActionResult> GetTotalCasosPorArbovirose([FromQuery] string disease)
    {
        var dto = new RelatorioDto
        {
            Disease = disease,
        };

        return await RealizarConsultaApi(dto);
    }

    /// <summary>
    /// Listar logs de acesso dos usuários.
    /// </summary>
    [HttpGet("listar-logs-acesso")]
    public IActionResult GetLogsAcesso()
    {
        // Lógica para consultar logs de acesso
        return Ok("Logs de acesso retornados.");
    }

    /// <summary>
    /// Listar logs de inclusão dos dados epidemiológicos.
    /// </summary>
    [HttpGet("listar-logs-inclusao-dados")]
    public IActionResult GetLogsInclusaoDados()
    {
        // Lógica para consultar logs de inclusão
        return Ok("Logs de inclusão de dados retornados.");
    }

    // Método auxiliar para realizar consulta à API externa
    private async Task<IActionResult> RealizarConsultaApi(RelatorioDto dto)
    {
        var baseUrl = _configuration["ApiDengue:Url"];

        var url = $"{baseUrl}?geocode={dto.Geocode}&disease={dto.Disease}&format={dto.Format}&ew_start={dto.EwStart}&ew_end={dto.EwEnd}&ey_start={dto.EyStart}&ey_end={dto.EyEnd}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Erro ao consultar a API externa.");
        }

        var result = await response.Content.ReadAsStringAsync();
        return Content(result, response.Content.Headers.ContentType?.ToString() ?? "application/json");
    }
}