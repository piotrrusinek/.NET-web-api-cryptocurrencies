using CryptoCurrencies.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CryptoCurrencies.Services;

public static class DataBaseService
{
    public static List<Currency> Load()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "data", "portfolio.json");
        if(File.Exists(path))
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Currency>>(json);
        }
        else return null;
        
    }

    public static bool Update(List<Currency> portfolio)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(portfolio);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "portfolio.json");
        var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if(!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
        File.WriteAllText(filePath, json);
        if (File.Exists(filePath)) return true;
        return false;
    }
}