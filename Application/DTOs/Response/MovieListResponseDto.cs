using Application.DTOs.Data;

namespace Application.DTOs.Response;

public class MovieListResponseDto
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
    public required IEnumerable<MovieDto> Movies { get; set; }
}
