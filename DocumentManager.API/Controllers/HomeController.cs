using DocumentManager.DTO.Models;
using DocumentManager.Services.Container;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DocumentManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IServiceContainer _serviceContainer;

        public HomeController(ILogger<HomeController> logger, IServiceContainer serviceContainer)
        {
            _logger = logger;
            _serviceContainer = serviceContainer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is the home page");
        }
    }
}
