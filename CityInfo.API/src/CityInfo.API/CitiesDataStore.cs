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
                    Description = "Capital of Germany",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id = 1,
                            Name = "Reichstag",
                            Description = "German government building."
                        },
                        new PointOfInterestDto
                        {
                            Id = 2,
                            Name = "The Berlin Wall",
                            Description = "Remains of the wall that divided East and West Berlin."
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Austin",
                    Description = "Capital of Texas",
                                        PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id = 3,
                            Name = "University of Texas",
                            Description = "The most prestigious public university in Texas."
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "Capital of France",
                                        PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id = 4,
                            Name = "Champs-Elysees",
                            Description = "World famous avenue in Paris."
                        },
                        new PointOfInterestDto
                        {
                            Id = 5,
                            Name = "The Louvre",
                            Description = "Museum of art."
                        }
                    }
                },
            };
        }
    }
}
