using CryptoCurrencies.Models;
using Newtonsoft.Json;

namespace CryptoCurrencies.Services;

public static class CurrencyService
{
    static List<Currency> Portfolio { get; }
    
    static CurrencyService()
    {
        var load = DataBaseService.Load();
        if(load is null) Portfolio = new List<Currency>();
        else Portfolio = new List<Currency>(load);
        
    }

    public static List<Currency> GetAll() => Portfolio;

    public static Currency? Get(string symbol) => Portfolio.FirstOrDefault(p => p.Symbol == symbol);

    public static void Add(Currency currency)
    {
        
        Portfolio.Add(currency);
        DataBaseService.Update(Portfolio);
    }

    public static void AutoAdd(string symbol, double amount)
    {
        double price = 63000.0; //fetch from binance
        BinanceService binanceService = new BinanceService();
        //string request = $"https://api.binance.com/api/v3/ticker/price?symbol={"BTCUSDT"}";
        string request = "https://api.binance.com/api/v3/ticker/price?symbol="  + (symbol.ToUpper()+"USDT");
        Console.WriteLine(request);
        var ticker = binanceService.GetAsync(request);
        var cryptoconverted = JsonConvert.DeserializeObject<BinanceTicker>(ticker.Result);
        Portfolio.Add(new Currency{ Symbol = symbol, Price = cryptoconverted.price, Amount = amount});
        DataBaseService.Update(Portfolio);
    }

    public static void Delete(string symbol)
    {
        var currency = Get(symbol);
        if(currency is null)
            return;

        Portfolio.Remove(currency);
    }

    public static void Update(Currency currency)
    {
        var index = Portfolio.FindIndex(p => p.Symbol == currency.Symbol);
        if(index == -1)
            return;

        Portfolio[index] = currency;
    }
}