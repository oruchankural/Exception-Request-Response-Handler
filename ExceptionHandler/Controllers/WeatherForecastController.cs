using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
       
            return "OK";

        }
        [HttpGet("Test")]
        public Test GetTest()
        {
            return new Test()
            {
                TestId = 1,
                TestName = "Test"
            };
        }

       [HttpPost("Test")]
       public String CreateTest([FromBody]Test test)
       {

            return $"Test data is created successfully TestId : {test.TestId} , TestName : {test.TestName}";
       }

        public class Test
        {
            public int TestId { get; set; }
            public string TestName { get; set; }
        }
    }
}
