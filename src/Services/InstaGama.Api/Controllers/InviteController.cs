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
    }
}
