﻿using Application.Dtos.UserDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondProject.Exceptions;
using SecondProject.Wrapper;

namespace SecondProject.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        //Controller used for authentication of users
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUsersService usersService, ITokenService tokenService, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {

            _usersService = usersService;
            _tokenService = tokenService;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [ResponseCache]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto userLoginCredentials)
        {
            _logger.LogInformation("Login attempt.");

            var user = await _usersService.GetUserAsync(userLoginCredentials);
            if (user == null)
            {
                _logger.LogInformation("Login failed");
                throw new IncorrectCredentialsException("Incorrect username or password.");

            }
            else
            {
                _logger.LogInformation("Login succeeded");
                var tokenString = _tokenService.GenerateJWT(user, _configuration);
                return Ok(new { token = tokenString });

            }

        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            _logger.LogInformation("Register attempt.");

            var user = await _usersService.GetUserByUsernameAsync(registerDto.Username);
            if (user != null)
            {
                _logger.LogError("Register failed, username has been already taken.");
                throw new IncorrectCredentialsException("Username has been already taken.");
            }
            var newUser = await _usersService.AddUserAsync(registerDto);
            if (newUser == null)
            {
                _logger.LogError("Register failed, incorrect credentials.");
                throw new IncorrectCredentialsException("Fields were not filled properly.");
            }
            return Created($"/register.{newUser.Username}", new Response<UserDto>(newUser));

        }

        [AllowAnonymous]
        [HttpPut("changePassword/{username}")]
        public async Task<ActionResult<UserDto>> ChangePassword(string username, string newPassword)
        {
            _logger.LogInformation($"Changing password of {username}");
            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogInformation("Changing password failed, username has not been found.");
                throw new IncorrectCredentialsException("Username has not been found in the database.");
            }
            var newUser = await _usersService.ChangePasswordAsync(username, newPassword);

            if (newUser == null)
            {
                _logger.LogError("Changing password failed, incorrect password format.");
                throw new EntityValidationException("Incorrect password format.");
            }
            return CreatedAtAction($"changePassword", new Response<UserDto>(newUser));

        }
        [AllowAnonymous]
        [HttpPut("changeEmail/{username}")]

        public async Task<ActionResult<UserDto>> ChangeEmail(string username, string newEmail)
        {
            _logger.LogInformation($"Changing email of {username}.");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogError("Changing password failed, username has not been found.");
                throw new IncorrectCredentialsException("Username has not been found in the database.");

            }
            var newUser = await _usersService.ChangeEmailAsync(username, newEmail);
            if (newUser == null)
            {
                _logger.LogInformation("Changing email failed, incorrect email format.");
                throw new EntityValidationException("Incorrect email format.");

            }
            return CreatedAtAction($"changeEmail", new Response<UserDto>(newUser));


        }
        [Authorize]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "username" })]
        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            _logger.LogInformation($"Getting user by username.");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogInformation("The user was not found in the database.");
                throw new EntityNotFoundException("The passed username is wrong - the usern doesnt't exist.");
            }

            return Ok(new Response<UserDto>(user));

        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            _logger.LogInformation($"Deleting user by username.");
            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogInformation("Deleting user failed, user was not found.");
                throw new EntityNotFoundException("The passed username is wrong - the usern doesnt't exist.");
            }

            await _usersService.DeleteUserAsync(username);
            return NoContent();
        }
    }
}
