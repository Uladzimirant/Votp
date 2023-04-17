using Microsoft.AspNetCore.Mvc;
using Votp.Contracts.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Votp.Controllers.ValidationAPI
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private ILogger<ValidationController> _logger;
        private ITokenCheckerService _checker;
        public ValidationController(ILogger<ValidationController> logger, ITokenCheckerService checker)
        {
            _logger = logger;
            _checker = checker;
        }


        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Check([FromForm] string user, [FromForm] string code)
        {
            return Ok(new { Valid = await _checker.Check(user, code) });
        }

        //// GET: api/<ValuesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}



        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
