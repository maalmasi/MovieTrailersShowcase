using Domain.Models.Data;

namespace Domain.Models.Response;

public class MovieTrailerResponse
{
    public int Id { get; set; }
    public IEnumerable<MovieTrailer> Results { get; set; } = [];
}
