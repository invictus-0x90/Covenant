﻿// Author: Ryan Cobb (@cobbr_io)
// Project: Covenant (https://github.com/cobbr/Covenant)
// License: GNU GPLv3

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Covenant.Core;
using Covenant.Models;
using Covenant.Models.Covenant;
using Covenant.Models.Listeners;

namespace Covenant.Controllers
{
    [Authorize(Policy = "RequireJwtBearer")]
    [ApiController]
    [Route("api/profiles")]
    public class ProfileApiController : Controller
    {
        private readonly CovenantContext _context;
        private readonly UserManager<CovenantUser> _userManager;

        public ProfileApiController(CovenantContext context, UserManager<CovenantUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/profiles
        // <summary>
        // Get a list of Profiles
        // </summary>
        [HttpGet(Name = "GetProfiles")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return Ok(await _context.GetProfiles());
        }

        // GET api/profiles/{id}
        // <summary>
        // Get a Profile by id
        // </summary>
        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            try
            {
                return await _context.GetProfile(id);
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        // POST api/profiles
        // <summary>
        // Create a Profile
        // </summary>
        [HttpPost(Name = "CreateProfile")]
        [ProducesResponseType(typeof(Profile), 201)]
        public async Task<ActionResult<Profile>> CreateProfile([FromBody]Profile profile)
        {
            try
            {
                Profile createdProfile = await _context.CreateProfile(profile, await _userManager.GetUserAsync(HttpContext.User));
                return CreatedAtRoute(nameof(GetProfile), new { id = createdProfile.Id }, createdProfile);
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        // PUT api/profiles
        // <summary>
        // Edit a Profile
        // </summary>
        [HttpPut(Name = "EditProfile")]
        public async Task<ActionResult<Profile>> EditProfile([FromBody] Profile profile)
        {
            try
            {
                return await _context.EditProfile(profile, await _userManager.GetUserAsync(HttpContext.User));
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        // DELETE api/profiles/{id}
        // <summary>
        // Delete a Profile
        // </summary>
        [HttpDelete("{id}", Name = "DeleteProfile")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteProfile(int id)
        {
            try
            {
                await _context.DeleteProfile(id);
                return new NoContentResult();
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        // GET: api/profiles/http
        // <summary>
        // Get a list of HttpProfiles
        // </summary>
        [HttpGet("http", Name = "GetHttpProfiles")]
        public async Task<ActionResult<IEnumerable<HttpProfile>>> GetHttpProfiles()
        {
            return Ok(await _context.GetHttpProfiles());
        }

        // GET api/profiles/http/{id}
        // <summary>
        // Get an HttpProfile by id
        // </summary>
        [HttpGet("http/{id}", Name = "GetHttpProfile")]
        public async Task<ActionResult<HttpProfile>> GetHttpProfile(int id)
        {
            try
            {
                return await _context.GetHttpProfile(id);
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        // POST api/profiles/http
        // <summary>
        // Create an HttpProfile
        // </summary>
        [HttpPost("http", Name = "CreateHttpProfile")]
        [ProducesResponseType(typeof(HttpProfile), 201)]
        public async Task<ActionResult<HttpProfile>> CreateHttpProfile([FromBody] HttpProfile profile)
        {
            try
            {
                HttpProfile createdProfile = await _context.CreateHttpProfile(profile, await _userManager.GetUserAsync(HttpContext.User));
                return CreatedAtRoute(nameof(GetHttpProfile), new { id = createdProfile.Id }, createdProfile);
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        // PUT api/profiles/http
        // <summary>
        // Edit a Profile
        // </summary>
        [HttpPut("http", Name = "EditHttpProfile")]
        public async Task<ActionResult<Profile>> EditHttpProfile([FromBody] HttpProfile profile)
        {
            try
            {
                return await _context.EditHttpProfile(profile, await _userManager.GetUserAsync(HttpContext.User));
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        // DELETE api/profiles/http/{id}
        // <summary>
        // Delete a HttpProfile
        // </summary>
        [HttpDelete("http/{id}", Name = "DeleteHttpProfile")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteHttpProfile(int id)
        {
            try
            {
                await _context.DeleteProfile(id);
                return new NoContentResult();
            }
            catch (ControllerNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ControllerBadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ControllerUnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
