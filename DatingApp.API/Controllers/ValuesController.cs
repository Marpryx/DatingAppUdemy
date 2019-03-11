using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    //GET http://localhost:5000/api/values/
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;  //_ to show that it's private fild. Not mandatory
        public ValuesController(DataContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        //we need to make the code asynhronius for the case of scalable app (many diff requests to DB)
        public async Task<IActionResult> GetValues() //Added async and Task - represents asynchronius operation that returns value
        {
            var values = await _context.Values.ToListAsync(); //use await and toListAsync

            return Ok(values);  //ok returns hhtp 200ok responce
            //throw new Exception("Test Exception");
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id) //also async method
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id); //x - value that we returning, default value = null + ok response
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
