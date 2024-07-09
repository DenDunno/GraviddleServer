namespace GraviddleServer.Code.Repository.Records;

[Serializable]
public record DeathRecord(string Name, string Level, string ScreenShot, string[] Reasons);