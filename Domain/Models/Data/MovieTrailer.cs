namespace Domain.Models.Data;

public class MovieTrailer
{
    public string? Name { get; set; }
    public required string Key { get; set; }
    public required string Site { get; set; }
    public required string Type { get; set; }
    public bool Official { get; set; }
    public string? Id { get; set; }
}
