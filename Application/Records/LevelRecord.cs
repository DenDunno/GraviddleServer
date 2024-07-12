using Domain.Repository;

namespace Application.Records;

[Serializable]
public record LevelRecord(string Id, string Name, int Stars, 
    string Level, int LevelIndex, double Time, int DeathCount)
    : IDatabaseModel<string>;