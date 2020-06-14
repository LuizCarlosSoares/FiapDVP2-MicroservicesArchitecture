namespace Authentication.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Options;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Domain.Services;
    using Domain.Settings;
    using Domain.Entities;
    using Authentication.Helpers;
    using Authentication.Models;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IdentityModel.Client;

    namespace WebApi.Controllers
    {
        [Authorize]
        [ApiController]
        [Route("[controller]")]
        public class UsersController : ControllerBase
        {
            private IUserService _userService;
            private IMapper _mapper;
            private readonly AuthSettings _authSettings;

            public UsersController(
                IUserService userService,
                IMapper mapper,
                IOptions<AuthSettings> authSettings)
            {
                _userService = userService;
                _mapper = mapper;
                _authSettings = authSettings.Value;
            }

            [AllowAnonymous]
            [HttpPost("authenticate")]
            public async Task<IActionResult> Authenticate([FromBody] UserAuthenticateModel model)
            {
                var user = _userService.Authenticate(model.Username, model.Password);

                if(user==null) 
                    return NotFound(user.Username);

                var client = new HttpClient();

                var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5001");
                if (disco.IsError)
                {
                
                    return Task.FromResult<IActionResult>(BadRequest(disco.Exception.Message)).Result;
                }

                // request token
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "e-commerce",
                    ClientSecret = "secret",

                    Scope = "Catalog-Api"
                });
                
                return Ok(new
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = tokenResponse.AccessToken
                });
            }

            [AllowAnonymous]
            [HttpPost("register")]
            public IActionResult Register([FromBody] UserRegisterModel model)
            {
                
                var user = _mapper.Map<User>(model);

                try
                {
                    
                    _userService.Create(user, model.Password);
                    return Ok();
                }
                catch (AppException ex)
                {
                    
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var users = _userService.GetAll();
                var model = _mapper.Map<IList<UserModel>>(users);
                return Ok(model);
            }

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var user = _userService.GetById(id);
                var model = _mapper.Map<UserModel>(user);
                return Ok(model);
            }

            [HttpPut("{id}")]
            public IActionResult Update(int id, [FromBody] UpdateUserModel model)
            {
                // map model to entity and set id
                var user = _mapper.Map<User>(model);
                user.Id = id;

                try
                {
                    // update user 
                    _userService.Update(user, model.Password);
                    return Ok();
                }
                catch (AppException ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                _userService.Delete(id);
                return Ok();
            }
        }
    }

}