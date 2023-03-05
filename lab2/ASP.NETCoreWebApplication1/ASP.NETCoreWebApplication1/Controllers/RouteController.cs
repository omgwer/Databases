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
        DbWorker worker = new DbWorker();

        //var test = new StopOnTheRoadDto
        //{
        //    IsHavePavilion = "Есть",
        //    BusStopName = "Нагорный",
        //    RangeFromStart = 2.33,
        //    PlacementAlongTheRoad = "Слева",
        //    Road = new RoadDto()
        //    {
        //        StartPoint = "yoshkar-ola",
        //        EndPoint = "morki"
        //    }
        //};
        
        //var test1 = new StopOnTheRoadDto
        //{
        //    IsHavePavilion = "Нет",
        //    BusStopName = "Остановка всякое",
        //    RangeFromStart = 1.488,
        //    PlacementAlongTheRoad = "Справа",
        //    Road = new RoadDto()
        //    {
        //        StartPoint = "shulka",
        //        EndPoint = "tetyshi"
        //    }
        //};

        //var kek = new DbWorker().executeRequest("SELECT * FROM  placement_along_the_road");


        //var result = new StopOnTheRoadDto[2] { test , test1};

        return Ok(worker.GetAllRoutes());
    }
}