using Application.Abstractions.Services;
using Application.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieTrailerController(IMovieTrailerService movieTrailerService) : ControllerBase
{
    [HttpGet]
    [Route(nameof(FindByQuery))]
    [ResponseCache(Duration = 86400)]
    public async Task<ActionResult<MovieListResponseDto>> FindByQuery(string query, int page, string lang = "en-US", bool displayAdult = false, string? region = null)
    {

        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest();
        }

        return await movieTrailerService.GetMovieTrailersAsync(query, page, lang, displayAdult, region);
    }

    [HttpGet]
    [Route(nameof(GetById))]
    [ResponseCache(Duration = 3600)]
    public async Task<ActionResult<MovieCompleteInfoResponseDto>> GetById(int id, string lang = "en-US")
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        MovieCompleteInfoResponseDto movie = await movieTrailerService.GetMovieCompleteInfoAsync(id, lang);

        return movie;
    }
}
