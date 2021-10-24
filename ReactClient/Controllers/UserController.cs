using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactClient.Dtos;
using ReactClient.Models;
using ReactClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UserController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        // GET: /api/user
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserViewModel>))]
        [ResponseCache(Duration = 1, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            Console.WriteLine("--> Getting users....");

            var users = await _usersService.GetAllUsersAsync();

            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        // GET: /api/user/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserViewModel>> GetUserById(int id)
        {
            Console.WriteLine("--> Getting user by id....");

            var user = await _usersService.GetUserByIdAsync(id);
            if (user != null)
            {
                return Ok(_mapper.Map<UserViewModel>(user));
            }

            return NotFound();
        }

        // PUT: /api/user/
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUser(UserViewModel userViewModel)
        {
            await _usersService.UpdateUserAsync(_mapper.Map<User>(userViewModel));
            return NoContent();
        }

        // DELETE: /api/user/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            bool isSuccess = await _usersService.DeleteUserAsync(id);

            if (isSuccess)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
