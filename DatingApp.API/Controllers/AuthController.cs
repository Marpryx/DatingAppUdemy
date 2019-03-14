using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]  //api/Auth
    [ApiController]
    public class AuthController : ControllerBase
    {
        //to inject a newly created repository to the controller we create a constructor
        // that is a structure of the controller class
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)       //(string username, string password) // username and password we will receive as an Object
        //need to create DTO (Data Transfer Object)for username and password
        {


            //validate request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower(); //to make a username not key sensitive
            if(await _repo.UserExists(userForRegisterDto.Username))
            return BadRequest("Username already exists"); //inherit from ControllerBase

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);// The await operator suspends execution until the work of the  method is complete

            return StatusCode(201); //location where to get a newely created entity
        }
    }
}