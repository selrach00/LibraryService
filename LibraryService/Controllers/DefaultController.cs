using System.Collections.Generic;
using LibraryService.Models;
using Microsoft.AspNetCore.Mvc;
using LibraryService.Database;
using Microsoft.Extensions.Configuration;
using System;

namespace LibraryService.Controllers
{

    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DefaultController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IEnumerable<Patron> Get()
        {
            var connectionString = _configuration["ConnectionString"];
            return Repository.SPOverdueBooksFromSunnydale(connectionString);
        }

        [HttpPost("RegisterPatron")]
        public ActionResult RegisterPatron(Patron patron)
        {
            var connectionString = _configuration["ConnectionString"];
            try
            {
                Repository.RegisterNewPatron(connectionString, patron);
                return new JsonResult($"{patron.FirstName} was successfully registered!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Found: {ex.Message}");
                return new JsonResult("Something went wrong, try again later!");
            }
        }
    }
}