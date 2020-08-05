namespace VueMVCCore.Controllers.api
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using VueMVCCore.Models;

    [ApiController]
    [Route(Constants.ApiRoute)]
    // Class needs the least restrictive attribute
    [Authorize(Constants.LoggedInPolicyName)]
    public class TestController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetWeather()
        {
            return new OkObjectResult(new List<string> { "windy", "stormy", "sunny" });
        }
    }
}