using AutoMapper;
using Manage.Api.Models;
using Manage.Core.DTOs;
using Manage.Core.Entities;
using Manage.Core.Services;
using Manage.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Manage.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, IMapper mapper, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReportDto>>> Get()
        {
            _logger.LogInformation("GET api/Report called");
            var list = await _reportService.GetAllAsync();
            var dtoList = list.Select(r => _mapper.Map<ReportDto>(r)).ToList();
            _logger.LogInformation($"GET api/Report returned {dtoList.Count} items");
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> Get(int id)
        {
            _logger.LogInformation($"GET api/Report/{id} called with id = {id}");
            var report = await _reportService.GetByIdAsync(id);
            if (report == null)
            {
                _logger.LogWarning($"GET api/Report/{id} - Report with id {id} not found");
                return NotFound();
            }
            var reportDto = _mapper.Map<ReportDto>(report);
            _logger.LogInformation($"GET api/Report/{id} returned report for id {id}");
            return Ok(reportDto);
        }

        [HttpPost]
        public async Task<ActionResult<ReportDto>> Post([FromBody] ReportPostModel report)
        {
            _logger.LogInformation("POST api/Report called with data: {@Report}", report);
            var reportToAdd = new Report
            {
                UserId = report.UserId,
                WorkDate = report.WorkDate,
                StartTime = report.StartTime,
                EndTime = report.EndTime
            };

            var newReport = await _reportService.AddAsync(reportToAdd);
            var reportDto = _mapper.Map<ReportDto>(newReport);
            _logger.LogInformation($"POST api/Report created report with id {reportDto?.Id}");
            return Ok(reportDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReportDto>> Put(int id, [FromBody] ReportPostModel report)
        {
            _logger.LogInformation($"PUT api/Report/{id} called with id = {id} and data: ", report);
            var updateReport = new Report
            {
                Id = id,
                UserId = report.UserId,
                WorkDate = report.WorkDate,
                StartTime = report.StartTime,
                EndTime = report.EndTime
            };

            var updated = await _reportService.UpdateAsync(updateReport);
            if (updated == null)
            {
                _logger.LogWarning($"PUT api/Report/{id} - Report with id {id} not found for update");
                return NotFound();
            }
            var reportDto = _mapper.Map<ReportDto>(updated);
            _logger.LogInformation($"PUT api/Report/{id} updated report with id {reportDto?.Id}");
            return Ok(reportDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation($"DELETE api/Report/{id} called with id = {id}");
            var existingReport = await _reportService.GetByIdAsync(id);
            if (existingReport == null)
            {
                _logger.LogWarning($"DELETE api/Report/{id} - Report with id {id} not found for deletion");
                return NotFound();
            }
            await _reportService.DeleteAsync(id);
            _logger.LogInformation($"DELETE api/Report/{id} deleted report with id {id}");
            return Ok();
        }
    }

}