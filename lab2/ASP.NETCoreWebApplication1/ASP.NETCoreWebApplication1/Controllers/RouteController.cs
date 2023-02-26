using ASP.NETCoreWebApplication1.Model;
using ASP.NETCoreWebApplication1.Model.DbWorker;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;

    public RouteController(ILogger<RouteController> logger)
    {
        _logger = logger;
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

        var kek = new DbWorker().executeRequest("SELECT * FROM  placement_along_the_road");


        var result = new RouteDto[2] { test , test1};

        return Ok(result);
    }
}