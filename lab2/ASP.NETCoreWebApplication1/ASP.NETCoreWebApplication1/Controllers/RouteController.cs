using ASP.NETCoreWebApplication1.Model;
using ASP.NETCoreWebApplication1.Model.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;
    private readonly StopOnTheRoadService _stopOnTheRoadService;

    public RouteController(ILogger<RouteController> logger, StopOnTheRoadService stopOnTheRoadService)
    {
        _logger = logger;
        _stopOnTheRoadService = stopOnTheRoadService;
    }

    [HttpGet]
    [Route("list/{index :int}")]
    public IActionResult Get(int index)
    {
        var test = new RouteDto
        {
            isHavePavilion = "Есть",
            busStopName = "Нагорный",
            rangeFromStart = 2.33,
            placementAlongTheRoad = "Слева",
            startPoint = "yoshkar-ola",
            finishPoint = "morki"
        };
        
        var test1 = new RouteDto
        {
            isHavePavilion = "Нет",
            busStopName = "Остановка всякое",
            rangeFromStart = 1.488,
            placementAlongTheRoad = "Справа",
            startPoint = "shulka",
            finishPoint = "tetyshi"
        };

            //  var kek = new DbWorker().executeRequest("SELECT * FROM  placement_along_the_road");

            var t = _stopOnTheRoadService.GetRecipe(0);
            
        var result = new RouteDto[2] { test , test1};

        return Ok(result);
    }
}