using Domain.Models.Data;

namespace Domain.Models.Response;

public class MovieInfoResponse
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
    public IEnumerable<MovieInfo> Results { get; set; } = [];
}
