namespace Application.DTOs.Data;

public class MovieDto
{
    public bool Adult { get; set; }
    public int Id { get; set; }
    public required string OriginalTitle { get; set; }
    public string? Title { get; set; } = null;
    public string? TrailerUrl { get; set; }
}
