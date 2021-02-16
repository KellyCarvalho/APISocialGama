using InstaGama.Application.AppFriends.Input;
using InstaGama.Application.AppFriends.Interfaces;
using InstaGama.Application.AppUser.Interfaces;
using InstaGama.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController: ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IFriendsAppService _friendAppService;

        public FriendController(IUserAppService userAppService, IFriendsAppService friendAppService)
        {
            _userAppService = userAppService;
            _friendAppService = friendAppService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FriendsInput friendInput)
        {
            try
            {
                var postage = await _friendAppService
                                    .InsertAsync(friendInput)
                                    .ConfigureAwait(false);

                return Created("", postage);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var friends = await _friendAppService
                                    .GetFriendsByUserIdAsync()
                                    .ConfigureAwait(false);

            if (friends is null)
                return NoContent();

            return Ok(friends);
        }



    
    }
}
