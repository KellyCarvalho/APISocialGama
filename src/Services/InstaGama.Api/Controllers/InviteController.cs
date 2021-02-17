using InstaGama.Application.AppInvite.Input;
using InstaGama.Application.AppInvite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteController: ControllerBase
    {

        private readonly IInviteAppService _inviteAppService;

        public InviteController(IInviteAppService inviteAppService)
        {
            _inviteAppService = inviteAppService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InviteInput inviteInput)
        {
            try
            {
                 var invite = await _inviteAppService
                                    .InsertAsync(inviteInput)
                                    .ConfigureAwait(false);

                return Created("", invite);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [HttpPut]
        [Route("{idfriend}")]
        public async Task<IActionResult> Update([FromRoute]int idfriend)
        {
            try
            {
                var invite = await _inviteAppService
                                   .UpdateAsync(idfriend)
                                   .ConfigureAwait(false);

                return Accepted("", invite);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await _inviteAppService
                                    .DeleteAsync(id)
                                    .ConfigureAwait(false);
                return Accepted("", "");
            }catch(ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }

        }

    }
}
