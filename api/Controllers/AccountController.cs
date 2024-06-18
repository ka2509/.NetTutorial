using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
            // có thể dùng trycatch ở đây
            if(!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
            if(user == null) return Unauthorized("Invalid username");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.Succeeded) return Unauthorized("Username not found/or password not correct");
            return Ok(_tokenService.CreateToken(user));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {
            try {
                if(!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if(createUser.Succeeded) {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded) {
                        var token = _tokenService.CreateToken(appUser);
                        return Ok(token);
                    }
                    else {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else {
                    return StatusCode(500, createUser.Errors);
                }
            } catch (Exception e) {
                return StatusCode(500, e);
            }
        }
    }
}