using CityInfo.API.Entities;
using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            // After defining this db seed method, it must be called in the
            // Startup class.

            if (context.Cities.Any())
            {
                // If data already exists then we don't want to see the db again.
                return;
            }

            // Initial seed data.
            var cities = new List<City>()
            {
                new City
                {
                    Name = "Dallas",
                    Description = "Heart of north Texas.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Sixth Floor Museum",
                            Description = "Memorial for JFK."
                        }
                    }
                },
                new City
                {
                    Name = "London",
                    Description = "Capital of England.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Big Ben",
                            Description = "It's a giant clock."
                        },
                        new PointOfInterest()
                        {
                            Name = "Buckingham Palace",
                            Description = "The Royal Family lives here."
                        }
                    }
                },
                new City
                {
                    Name = "Berlin",
                    Description = "This city had a wall.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Autobahn",
                            Description = "The German super highway."
                        },
                        new PointOfInterest()
                        {
                            Name = "Checkpoint Charlie",
                            Description = "Cold War crossroad."
                        }
                    }
                },
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
