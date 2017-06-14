using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new CityDto
                {
                    Id = 1,
                    Name = "Dallas",
                    Description = "Heart of north Texas.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Sixth Floor Museum",
                            Description = "Memorial for JFK."
                        }
                    }
                },
                new CityDto
                {
                    Id = 2,
                    Name = "London",
                    Description = "Capital of England.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Big Ben",
                            Description = "It's a giant clock."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Buckingham Palace",
                            Description = "The Royal Family lives here."
                        }
                    }
                },
                new CityDto
                {
                    Id = 3,
                    Name = "Berlin",
                    Description = "This city had a wall.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Autobahn",
                            Description = "The German super highway."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Checkpoint Charlie",
                            Description = "Cold War crossroad."
                        }
                    }
                },
            };
        }
    }
}
