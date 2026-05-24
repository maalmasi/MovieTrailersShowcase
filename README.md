# Movie Trailers Showcase

This is a .NET 10 WebAPI showcase project. It was done in a 4 hour window as was the requirement of the challenge.
It allows a user to fetch movies from TMDB by search query or ID.
It will append the URL of the movie trailer to every movie in all requests.

## Prerequisites

- .NET 10 SDK
- Docker (optional)

## Running
- Build and run
- Docker

## Technology

#### Architecture
- Clean architecture
    - Ensures scalability
    - CQRS and Mediatr patterns were omitted to save (mostly) on dev time and overhead, since the application currently has no persistance

#### Caching

- .NET integrated cache using `[ResponseCache]` decorators
    - This decision was made to save on dev time. 
    - In real production environments a distributed cache should be used, such as Redis.

#### Error handling 
- Integrated `IExceptionHandler`

#### HTTP
- Polly
    - API resilience
