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

            if(friends is null)
            {
                return NoContent();
            }

            return Ok(friends);
        }



        [Authorize]
        [HttpGet]
        [Route("Pending")]
        public async Task<IActionResult> GetPending()
        {
            var friends = await _friendAppService
                                    .GetFriendsByFriendPendingAsync()
                                    .ConfigureAwait(false);
                                    

            if (friends is null)
            {
                return NoContent();
            }

            return Ok(friends);
        }

        [Authorize]
        [HttpGet]
        [Route("Pending/{idfriend}")]
        public async Task<IActionResult> GetPendingIdfriend(int idfriend)
        {
            var friends = await _friendAppService
                                    .GetFriendsByFriendIdPendingAsync(idfriend)
                                    .ConfigureAwait(false);


            if (friends is null)
            {
                return NoContent();
            }

            return Ok(friends);
        }

        [Authorize]
        [HttpGet]
        [Route("{idfriend}")]
        public async Task<IActionResult> GetIdfriend(int idfriend)
        {
            var friends = await _friendAppService
                                    .GetFriendsByFriendIdAsync(idfriend)
                                    .ConfigureAwait(false);


            if (friends is null)
            {
                return NoContent();
            }

            return Ok(friends);
        }

        [Authorize]
        [HttpGet]
        [Route("Profile/{idfriend}")]
        public async Task<IActionResult> GetProfile(int idfriend)
        {
            var friends = await _friendAppService
                                    .GetProfileFriendById(idfriend)
                                    .ConfigureAwait(false);


            if (friends is null)
            {
                return NoContent();
            }

            return Ok(friends);
        }

        [Authorize]
        [HttpGet]
        [Route("Profile/")]
        public async Task<IActionResult> GetProfileFriends()
        {
            var friends = await _friendAppService
                                    .GetProfileAllFriends()
                                    .ConfigureAwait(false);


            if (friends is null)
            {
                return NoContent();
            }

            return Ok(friends);
        }

        [HttpPut]
        [Route("{idfriend}/Acceptfriendship")]
        public async Task<IActionResult> Update([FromRoute] int idfriend)
        {
            try
            {
                var friend = await _friendAppService
                                      .UpdateAsync(idfriend)
                                      .ConfigureAwait(false);

                return Accepted("", friend);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [HttpDelete]
        [Route("{idFriend}")]
        public async Task<IActionResult> Delete([FromRoute] int idFriend)
        {
            try
            {
                await _friendAppService
                                    .DeleteAsync(idFriend)
                                    .ConfigureAwait(false);
                return Accepted("", "");
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("AllPhotosFriends/")]
        public async Task<IActionResult> GetAllPhotosFriends()
        {
            var allPhotosFriends = await _friendAppService
                                    .GetPhotosFriendsAsync()
                                    .ConfigureAwait(false);

            if (allPhotosFriends is null)
            {
                return NoContent();
            }

            return Ok(allPhotosFriends);
        }

        [Authorize]
        [HttpGet]
        [Route("{idFriend}/PhotosFriend/")]
        public async Task<IActionResult> GetPhotosFriendById(int idFriend)
        {
            var allPhotosFriend = await _friendAppService
                                    .GetPhotosFriendByIdAsync(idFriend)
                                    .ConfigureAwait(false);

            if (allPhotosFriend is null)
            {
                return NoContent();
            }

            return Ok(allPhotosFriend);
        }

    }
}
