using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
                                            IMailService mailService,
                                            ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when retrieving points of interest.");
                    return NotFound();
                }

                var pointsOfInterest = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

                var pointsOfInterestForCityResult = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest);

                return Ok(pointsOfInterestForCityResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", ex);

                return StatusCode(500, "Something went wrong when handling your request.");
            }


        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when retrieving point of interest.");
                    return NotFound();
                }

                var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

                if (pointOfInterestEntity == null)
                {
                    return NotFound();
                }

                var pointOfInterestForCityResult = Mapper.Map<PointOfInterestDto>(pointOfInterestEntity);

                return Ok(pointOfInterestForCityResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interest for city with id {cityId}", ex);
                return StatusCode(500, "Something went wrong when handling your request.");
            }


        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The Description should be different from the Name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }


            var finalPointOfInterest = Mapper.Map<PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestFor(cityId, finalPointOfInterest);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Something went wrong when handling your request.");
            }

            var createdPointOfInterestToReturn = Mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The Description should be different from the Name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            // AutoMapper automatically takes the changes on the source object and update them
            // to the destination object.
            Mapper.Map(pointOfInterest, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Something went wrong when handling your request.");
            }

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description", "The Description should be different from the Name.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // AutoMapper automatically takes the changes on the source object and update them
            // to the destination object.
            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Something went wrong when handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            _mailService.Send("Point of interest deleted", $"Point of interest {pointOfInterestFromStore.Name} with id of {pointOfInterestFromStore.Id} was deleted.");

            return NoContent();
        }
    }
}
