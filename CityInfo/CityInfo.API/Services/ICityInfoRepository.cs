using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int cityId);

        IEnumerable<PointOfInterest> GetPointsOfInterest(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

    }
}
