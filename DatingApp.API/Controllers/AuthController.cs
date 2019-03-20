using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]  //api/Auth
    [ApiController]
    public class AuthController : ControllerBase
    {
        //to inject a newly created repository to the controller we create a constructor
        // that is a structure of the controller class
        private readonly IAuthRepository _repo;  //_ for private field
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config) //inject IConfiguration, 
        // initialize field from parameter
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)       //(string username, string password) // username and password we will receive as an Object
        //need to create DTO (Data Transfer Object)for username and password
        {


            //validate request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower(); //to make a username not key sensitive
            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists"); //inherit from ControllerBase

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);// The await operator suspends execution until the work of the  method is complete

            return StatusCode(201); //location where to get a newely created entity
        }


        //to allow registered user to login to app. We need to return token to user once he logged in 
        //then he will be able to use this token to autorize themselfs 

        [HttpPost("login")] //login - path
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //checked the the username and pass mathes username and pass stored in DB
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            //building a token that we will return to user. It should contain userId and UserName
            //token can be verified by server even don't use the DB

            var claims = new[]
            {
                //token has 2 claims: UsetId and UserName
            new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromRepo.Username)
        };

        //in order to be shure we taking a valid token when it comes back, we need to sign this token
        //key to sign the token
        //key - is a security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            
            //we using security key as a part of SigningCredentials and incripting the key with the HmacSha512Signature algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //expires in 24 hours//1 should be a var, but for now it's ok
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok( new {
                token = tokenHandler.WriteToken(token)
            });

    }

    }
}