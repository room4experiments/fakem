using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Json;
using System;
using MathWorks.MATLAB.NET.Arrays;

namespace fakem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatlabCallController : ControllerBase
    {
        private readonly ILogger<MatlabCallController> _logger;

        public MatlabCallController(ILogger<MatlabCallController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Matlab")]
        public async Task<IActionResult> Get()
        {

            using (fake_json.fake1 obj = new fake_json.fake1())
            {
                MWArray inputString = new MWCharArray("[]");
                MWArray res = obj.fake_json(inputString);
                return Ok(JsonSerializer.Serialize(res.ToString()));
            }
        }
    }
}