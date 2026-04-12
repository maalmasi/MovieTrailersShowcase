using Application.DTOs.Data;

namespace Application.DTOs.Response;

public class MovieCompleteInfoResponseDto : MovieDto
{
    public string? BackdropPath { get; set; }
    public CollectionDto? BelongsToCollection { get; set; }
    public int Budget { get; set; }
    public List<GenreDto>? Genres { get; set; }
    public string? Homepage { get; set; }
    public string? ImdbId { get; set; }
    public List<string>? OriginCountry { get; set; }
    public string? OriginalLanguage { get; set; }
    public string? Overview { get; set; }
    public double Popularity { get; set; }
    public string? PosterPath { get; set; }
    public List<ProductionCompanyDto>? ProductionCompanies { get; set; }
    public List<ProductionCountryDto>? ProductionCountries { get; set; }
    public DateTime ReleaseDate { get; set; }
    public long Revenue { get; set; }
    public int Runtime { get; set; }
    public List<SpokenLanguageDto>? SpokenLanguages { get; set; }
    public string? Status { get; set; }
    public string? Tagline { get; set; }
    public bool Video { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
}
public class CollectionDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PosterPath { get; set; }
    public string? BackdropPath { get; set; }
}

public class GenreDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class ProductionCompanyDto
{
    public int Id { get; set; }
    public string? LogoPath { get; set; }
    public string? Name { get; set; }
    public string? OriginCountry { get; set; }
}

public class ProductionCountryDto
{
    public string? Iso3166_1 { get; set; }
    public string? Name { get; set; }
}

public class SpokenLanguageDto
{
    public string? EnglishName { get; set; }
    public string? Iso639_1 { get; set; }
    public string? Name { get; set; }
}