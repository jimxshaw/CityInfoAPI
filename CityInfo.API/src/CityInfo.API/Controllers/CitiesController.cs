using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    public class CitiesController : Controller
    {
        [HttpGet("api/cities")]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>()
            {
                new { Id = 1, Name = "Dallas" },
                new { Id = 2, Name = "Seattle" }
            });
        }
    }
}
