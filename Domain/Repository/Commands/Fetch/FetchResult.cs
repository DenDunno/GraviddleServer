namespace Domain.Repository.Commands;

public record FetchResult<T>(bool Success, T Record);