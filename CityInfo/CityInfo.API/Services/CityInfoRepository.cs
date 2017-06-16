using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            throw new NotImplementedException();
        }

        public City GetCity(int cityId)
        {
            throw new NotImplementedException();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterest(int cityId)
        {
            throw new NotImplementedException();
        }
    }
}
