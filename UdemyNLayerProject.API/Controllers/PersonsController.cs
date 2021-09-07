using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IService<Person> _personService;

        public PersonsController(IService<Person> personService)
        {
            _personService = personService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();

            return Ok(persons);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person model)
        {
            var persons = await _personService.AddAsync(model);

            return Created(String.Empty, persons);
        }
    }
}
