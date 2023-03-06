using Lab2.DbWorker;
using Lab2.Model.Domain;
using Lab2.Model.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;
    private readonly Lab2.DbWorker.DbWorker _dbWorker;

    public RouteController(ILogger<RouteController> logger, Lab2.DbWorker.DbWorker dbWorker)
    {
        _logger = logger;
        _dbWorker = dbWorker;
    }

    [HttpGet]
    [Route("list/{index :int}")]
    public IActionResult Get(int offset = 0, int limit = 10)
    {

        var response = _dbWorker.GetStopOnTheRoadList(offset, limit);
        return Ok(response);
    }

    [HttpGet]
    [Route("restrictions")]
    public IActionResult GetRestrictions()
    {
        var placementRestriction = _dbWorker.GetAllPlacementAlongTheRoad();//_restrictionService.GetPlacementOfTheRoadRestriction();
        var localityNameRestriction = _dbWorker.GetLocalityNameRestriction();//_restrictionService.GetLocalityNameRestriction();
        var busStopNameRestriction = _dbWorker.GetBusStopNameRestriction();//_restrictionService.GetBusStopNameRestriction();
        var isHavePavilionRestriction = _dbWorker.GetIsHavePavilionRestriction();//_restrictionService.GetIsHavePavilionRestriction();

        var restriction = new Restriction()
        {
            PlacementRestriction = placementRestriction,
            LocalityName = localityNameRestriction,
            BusStopName = busStopNameRestriction,
            IsHavePavilion = isHavePavilionRestriction
        };

        return Ok(restriction);
    }

    [HttpPost]
    [Route("searchSubstring")]
    public IActionResult SearchSubstring(SearchSubstringRequest request)
    {
        if (request.Substring == "")
            return NotFound();
        var response = _dbWorker.SearchSubstring(request.Offset, request.Limit, request.Substring);
        return Ok(response);
    }
 
}