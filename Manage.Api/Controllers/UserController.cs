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

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            

            _logger.LogInformation($"Base directory: {AppContext.BaseDirectory}");
            _logger.LogInformation("GET api/user called");
            var list = await _userService.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<UserDto>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            _logger.LogInformation("GET api/user/{id} called with id = {Id}", id);
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with id = {Id} not found", id);
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserPostModel user)
        {
            _logger.LogInformation("POST api/user called");
            try
            {
                var userToAdd = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };

                var newUser = await _userService.AddAsync(userToAdd);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UserPostModel user)
        {
            _logger.LogInformation("PUT api/user/{id} called with id = {Id}", id);
            try
            {
                var userToUpdate = new User
                {
                    Id = id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };

                var updatedUser = await _userService.UpdateAsync(userToUpdate);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user with id = {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("DELETE api/user/{id} called with id = {Id}", id);
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user with id = {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
