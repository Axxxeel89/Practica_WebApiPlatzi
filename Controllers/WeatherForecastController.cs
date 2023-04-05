using Microsoft.AspNetCore.Mvc;

namespace Practica_API.Controllers;

[ApiController]
[Route("api/[controller]")] //--> api es un valor fijo y lo que esta en corchetes son valores que son dinamicos, por ejemplo el controlador. 
//Esto es enrutamiento a nivel de controlador
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;


    private static List<WeatherForecast>  ListWeatherForecast = new List<WeatherForecast>();

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;

        if(ListWeatherForecast == null || !ListWeatherForecast.Any()) //-> Any me indica si tiene algun registro la lista. 
        { 
            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        }
    }

    [HttpGet]
    [Route("Get/WeatherForecast")] //--> Podemos agregar rutas a nivel de acciones
    [Route("Get/WeatherForecast2")] //--> Podemos agregar mas de una ruta y ambas usarian la misma accion
    [Route("[action]")] //-> Este usa el nombre que tiene la accion por ejemplo en este caso Get
    public IEnumerable<WeatherForecast> Get()
    {
        return ListWeatherForecast;
    }

 

    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    { 
        ListWeatherForecast.Add(weatherForecast);

        return Ok();
    } 

    [HttpDelete("{index}")] //-->Indicamos que vamos a recibir un valor por URL
    public IActionResult Delete(int index)
    {
        ListWeatherForecast.RemoveAt(index);
 
        return Ok();
    }


}
