using System.Text;
using System.Text.Json;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration
{
  public class ChartWrapper
  {
    private static readonly HttpClient Client = new HttpClient();

    public int Width { get; set; }

    public int Height { get; set; }

    public double DevicePixelRatio { get; set; }

    public string Format { get; set; }

    public string BackgroundColor { get; set; }

    public string Key { get; set; }

    public string Version { get; set; }

    public string Config { get; set; }

    public string Scheme { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public ChartWrapper(string scheme = null, string host = null, int? port = null)
    {
      Width = 500;
      Height = 300;
      DevicePixelRatio = 1.0;
      Format = "png";
      BackgroundColor = "transparent";
      if (host != null)
      {
        Host = host;
        if (scheme != null)
        {
          Scheme = scheme;
          if (port.HasValue)
            Port = port.Value;
          else
            Port = !(scheme == "http") ? 443 : 80;
        }
        else
        {
          Scheme = "https";
          Port = 443;
        }
      }
      else
      {
        Scheme = "https";
        Host = "quickchart.io";
        Port = 443;
      }
    }

    public async Task<byte[]> ToByteArray()
    {
      if (Config == null)
        throw new NullReferenceException("You must set Config on the QuickChart object before generating a URL");
      string content = JsonSerializer.Serialize(new
      {
        width = Width,
        height = Height,
        backgroundColor = BackgroundColor,
        devicePixelRatio = DevicePixelRatio,
        format = Format,
        chart = Config,
        key = Key,
        version = Version
      }, new JsonSerializerOptions()
      {
        IgnoreNullValues = true
      });
      string requestUri = string.Format("{0}://{1}:{2}/chart", Scheme, Host, Port);
      HttpResponseMessage result = await Client.PostAsync(requestUri, new StringContent(content, Encoding.UTF8, "application/json"));
      if (!result.IsSuccessStatusCode)
        throw new Exception("Unsuccessful response from API");
      return await result.Content.ReadAsByteArrayAsync();
    }
  }
}
