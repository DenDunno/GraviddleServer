namespace GraviddleServer.Code.API;

[Serializable]
public record DeathRecord(string Name, string Level, string ScreenShot, string[] Reasons);