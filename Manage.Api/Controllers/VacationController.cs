using AutoMapper;
using Manage.Api.Models;
using Manage.Core.DTOs;
using Manage.Core.Entities;
using Manage.Core.Services;
using Manage.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Manage.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IVacationService _vacationService;
        private readonly IMapper _mapper;
        private readonly ILogger<VacationController> _logger; 

        public VacationController(IVacationService vacationService, IMapper mapper, ILogger<VacationController> logger) 
        {
            _vacationService = vacationService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<VacationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacationDto>>> Get()
        {
            _logger.LogInformation("GET api/Vacation called");
            var list = await _vacationService.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<VacationDto>>(list);
            _logger.LogInformation($"GET api/Vacation returned {dtoList.Count()} items");
            return Ok(dtoList);
        }

        // GET api/<VacationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VacationDto>> Get(int id)
        {
            _logger.LogInformation($"GET api/Vacation/{id} called with id = {id}");
            var vacation = await _vacationService.GetByIdAsync(id);
            if (vacation == null)
            {
                _logger.LogWarning($"GET api/Vacation/{id} - Vacation with id {id} not found");
                return NotFound();
            }
            var vacationDto = _mapper.Map<VacationDto>(vacation);
            _logger.LogInformation($"GET api/Vacation/{id} returned vacation for id {id}");
            return Ok(vacationDto);
        }

        // POST api/<VacationController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VacationPostModel vacation)
        {
            _logger.LogInformation("POST api/Vacation called with data: {@Vacation}", vacation);
            var vacationToAdd = new Vacation
            {
                UserId = vacation.UserId,
                ReportDate = vacation.ReportDate,
                Request = vacation.Request
            };

            var newVacation = await _vacationService.AddAsync(vacationToAdd);
            _logger.LogInformation($"POST api/Vacation created vacation with id {newVacation?.Id}");
            return Ok(newVacation);
        }

        // PUT api/<VacationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] VacationPostModel vacation)
        {
            _logger.LogInformation($"PUT api/Vacation/{id} called with id = {id} and data: ", vacation);
            var vacationToUpdate = new Vacation
            {
                Id = id,
                UserId = vacation.UserId,
                ReportDate = vacation.ReportDate,
                Request = vacation.Request
            };

            var updatedVacation = await _vacationService.UpdateAsync(vacationToUpdate);
            if (updatedVacation == null)
            {
                _logger.LogWarning($"PUT api/Vacation/{id} - Vacation with id {id} not found for update");
                return NotFound(); 
            }
            _logger.LogInformation($"PUT api/Vacation/{id} updated vacation with id {id}");
            return Ok(updatedVacation);
        }

        // DELETE api/<VacationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation($"DELETE api/Vacation/{id} called with id = {id}");
            var existingVacation = await _vacationService.GetByIdAsync(id);
            if (existingVacation == null)
            {
                _logger.LogWarning($"DELETE api/Vacation/{id} - Vacation with id {id} not found for deletion");
                return NotFound(); 
            }
            await _vacationService.DeleteAsync(id);
            _logger.LogInformation($"DELETE api/Vacation/{id} deleted vacation with id {id}");
            return Ok();
        }
    }

}