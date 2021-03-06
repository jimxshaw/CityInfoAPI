﻿using CityInfo.API.Entities;
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

        City GetCity(int cityId, bool includePointsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

        void AddPointOfInterestFor(int cityId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        bool Save();

        bool CityExists(int cityId);
    }
}
