using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Berlin",
                    Description = "Capital of Germany"
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Austin",
                    Description = "Capital of Texas"
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "Capital of France"
                },
            };
        }
    }
}
