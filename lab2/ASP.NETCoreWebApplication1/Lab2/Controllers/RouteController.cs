using Lab2.Model.Domain;
using Lab2.Model.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;
    private readonly StopOnTheRoadService _stopOnTheRoadService;
    private readonly RestrictionService _restrictionService;

    public RouteController(ILogger<RouteController> logger, StopOnTheRoadService stopOnTheRoadService,
        RestrictionService restrictionService)
    {
        _logger = logger;
        _stopOnTheRoadService = stopOnTheRoadService;
        _restrictionService = restrictionService;
    }

    [HttpGet]
    [Route("list/{index :int}")]
    public IActionResult Get(int offset = 0, int limit = 10)
    {
        var response = _stopOnTheRoadService.GetStopOnTheRoadList(offset, limit);
        return Ok(response);
    }

    [HttpGet]
    [Route("restrictions")]
    public IActionResult GetRestrictions()
    {
        var placementRestriction = _restrictionService.GetPlacementOfTheRoadRestriction();
        var localityNameRestriction = _restrictionService.GetLocalityNameRestriction();
        var busStopNameRestriction = _restrictionService.GetBusStopNameRestriction();
        var isHavePavilionRestriction = _restrictionService.GetIsHavePavilionRestriction();

        var restriction = new Restriction()
        {
            PlacementRestriction = placementRestriction,
            LocalityName = localityNameRestriction,
            BusStopName = busStopNameRestriction,
            IsHavePavilion = isHavePavilionRestriction
        };

        return Ok(restriction);
    }
}