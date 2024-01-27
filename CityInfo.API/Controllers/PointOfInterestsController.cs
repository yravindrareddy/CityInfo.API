using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CityInfo.API.Controllers
{
    
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointofinterests")]
    //[Authorize(Roles = "MustBeFromDelhi")]
    [ApiController]
    public class PointOfInterestsController : ControllerBase
    {
        private readonly ILogger<PointOfInterestsController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestsController(ILogger<PointOfInterestsController> logger, 
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId)
        {
            try
            {
                var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

                if(!await _cityInfoRepository.CityNameMatchesCityId(cityId, cityName))
                {
                    return Forbid();
                }

                var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
                if (!cityExists)
                {
                    _logger.LogInformation($"Could not find city with cityId {cityId}");
                    return NotFound();
                }
                var pointOfInterests = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterests));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interests for cityId {cityId}.", ex);
                return StatusCode(500, "An error occured while getting point of interests");
            }
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointofinterestid)
        {
            try
            {               
                
                if (!await _cityInfoRepository.CityExistsAsync(cityId))
                {
                    return NotFound();
                }
                var pointofInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestid);
                if (pointofInterest == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<PointOfInterestDto>(pointofInterest));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interests for cityId {cityId}.", ex);
                return StatusCode(500, "An error occured while getting point of interests");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestCreateDto pointOfInterestCreateDto)
        {            
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var newPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterestCreateDto);
                        
            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId , newPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestDto = _mapper.Map<PointOfInterestDto>(newPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", 
                new
                {
                    cityId = cityId,
                    pointofinterestid = createdPointOfInterestDto.Id
                },
                createdPointOfInterestDto);
        }

        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdateDto)
        {            
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

             _mapper.Map(pointOfInterestUpdateDto, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, 
            JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestTopatch = _mapper.Map<PointOfInterestUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestTopatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestTopatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestTopatch, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointofInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();
            _mailService.Send("PointOfInterest Deleted", $"PointOfInterest Id {pointOfInterestEntity.Id} deleted from City Id {pointOfInterestEntity.CityId}");

            return NoContent();
        }
    }
}
