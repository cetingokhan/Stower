# Stower
## Stacks incoming data and toppling it for batch operation when it comes to the stack limit



Microsoft.Extensions.DependencyInjection


```csharp 
services.AddStower(options =>
{
    options.MaxStackLenght = Convert.ToInt32(_configuration["Stower:MaxStackLenght"]);
    options.MaxWaitInSecond = Convert.ToInt32(_configuration["Stower:MaxWaitInSecond"]);
    options.Stacks = new List<Stower.Base.ICustomStack>()
    {
        new CustomStack<WeatherData>()
    };
}).AddToppleHandler<ToppleHandler>();
```


Sample injection and stack received data

```csharp 
[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase    
{
    private readonly IStower _stower;

    public WeatherController(IStower stower)
    {
        _stower = stower;
    }
    [HttpPost]
    public async Task<IActionResult> AddNew(WeatherData item)
    {
        await _stower.Add<WeatherData>(item);
        return Ok();
    }
}
```

Topple Handler -> It calls when stack count comes to the limit
```csharp 
public class ToppleHandler : IToppleHandler
{
    public async Task Handle(List<object> objects)
    {

    }
}
```
