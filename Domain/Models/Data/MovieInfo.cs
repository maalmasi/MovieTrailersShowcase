namespace Domain.Models.Data;

public class MovieInfo
{
    public bool Adult { get; set; }
    public int Id { get; set; }
    public string? OriginalLanguage { get; set; } = null;
    public required string OriginalTitle { get; set; }
    public string? PosterPath { get; set; } = null;
    public string? Title { get; set; } = null;
}
