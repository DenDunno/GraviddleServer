using TelegramBotNM.Repository;

namespace GraviddleServer.Code.API;

[Serializable]
public record LevelRecord(string Id, string Name, int Stars, 
    string Level, int LevelIndex, double Time, int DeathCount)
    : IDatabaseModel<string>;