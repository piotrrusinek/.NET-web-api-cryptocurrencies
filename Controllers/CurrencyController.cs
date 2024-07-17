using CryptoCurrencies.Models;
using CryptoCurrencies.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencies.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    public CurrencyController()
    {
    }

    [HttpGet]
    public ActionResult<List<Currency>> GetAll() =>
        CurrencyService.GetAll();

    [HttpGet("{symbol}")]
    public ActionResult<Currency> Get(string symbol)
    {
        
        var currency = CurrencyService.Get(symbol);

        if(currency == null)
            return NotFound();

        return currency;
    }

    [HttpPost]
    public IActionResult Create(Currency currency)
    {            
        CurrencyService.Add(currency);
        return CreatedAtAction(nameof(Get), new { symbol = currency.Symbol }, currency);
    }

    [HttpPost("{symbol}")]
    public IActionResult Create(string symbol, AutoCurrency autoCurrency)
    {            
        CurrencyService.AutoAdd(symbol, autoCurrency.Amount);
        return CreatedAtAction(nameof(Get), new { symbol = symbol }, CurrencyService.Get(symbol));
    }

    [HttpPut("{symbol}")]
    public IActionResult Update(string symbol, Currency currency)
    {
        if (symbol != currency.Symbol)
            return BadRequest();
           
        var existingCurrency = CurrencyService.Get(symbol);
        if(existingCurrency is null)
            return NotFound();
   
        CurrencyService.Update(currency);           
   
        return NoContent();
    }

    [HttpDelete("{symbol}")]
    public IActionResult Delete(string symbol)
    {
        var currency = CurrencyService.Get(symbol);
   
        if (currency is null)
            return NotFound();

        CurrencyService.Delete(symbol);
   
        return NoContent();
    }
}