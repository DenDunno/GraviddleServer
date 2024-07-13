namespace Domain.Repository.Commands.Fetch;

public record FetchResult<T>(bool Success, T Record);