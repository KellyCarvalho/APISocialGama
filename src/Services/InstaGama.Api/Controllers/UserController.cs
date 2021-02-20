using InstaGama.Application.AppUser.Input;
using InstaGama.Application.AppUser.Interfaces;
using InstaGama.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInput userInput)
        {
            try
            {
                var user = await _userAppService
                                    .InsertAsync(userInput)
                                    .ConfigureAwait(false);

                return Created("", user);
            }
            catch(ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userAppService
                                .GetByIdAsync(id)
                                .ConfigureAwait(false);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        [Route("Photos/")]
        public async Task<IActionResult> GetPhotos()
        {
            var photos = await _userAppService
                                .GetPhotosUserAsync()
                                .ConfigureAwait(false);

            if (photos is null)
                return NotFound();

            return Ok(photos);
        }

        
        [HttpGet]
        [Route("AllUsers/")]
        public async Task<IActionResult> GetAllUsers()
        {
            var photos = await _userAppService
                                .GetAllUsersAsync()
                                .ConfigureAwait(false);

            if (photos is null)
                return NotFound();

            return Ok(photos);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserInput userInput)
        {
            try
            {

                var userUpdated = await _userAppService
                                    .UpdateAsync(userInput)
                                    .ConfigureAwait(false);

                return Accepted("", userUpdated);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await _userAppService
                            .DeleteUserAsync()
                            .ConfigureAwait(false);
                return Accepted("", "");
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        }
}
