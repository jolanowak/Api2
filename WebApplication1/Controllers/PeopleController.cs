using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;
        public PeopleController(PeopleContext context)
        {
            _context = context;
           // if (_context.People.Count() == 0)
            //{
             //   _context.People.Add(new People { Id = 1, Email = "bozenka15@gmail.com", IsActive = true, Name = "Bożena", Surname = "Kocioł", Phone = "998567021" });
               // _context.People.Add(new People { Id = 2, Email = "weronika.zbik@gmail.com", IsActive = false, Name = "Weronika", Surname = "Żbik", Phone = "678022345" });
                //_context.People.Add(new People { Id = 3, Email = "fryderykchopin@wp.pl", IsActive = true, Name = "Fryderyk", Surname = "Chopin", Phone = "770335721" });
                //_context.SaveChanges();
            //}
        }

        [HttpGet]
        public ActionResult<IEnumerable<People>> GetPeople()
        {
            try
            {
                var people = _context.People.ToList();

                if (people == null || people.Count == 0)
                {
                    return NotFound("No people found.");
                }

                return people;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving people: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<People> GetPerson(int id)
        {
            try
            {
                var person = _context.People.FirstOrDefault(p => p.Id == id);

                if (person == null)
                {
                    return NotFound("Person not found.");
                }

                return person;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving person: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<People> PostPeople(People people)
        {
            try
            {
                _context.People.Add(people);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetPeople), new { id = people.Id }, people);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating person: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePeople(int id, People updatedPeople)
        {
            try
            {
                var people = _context.People.FirstOrDefault(p => p.Id == id);
                if (people == null)
                {
                    return NotFound("Person not found.");
                }

                people.Email = updatedPeople.Email;
                people.IsActive = updatedPeople.IsActive;
                people.Name = updatedPeople.Name;
                people.Surname = updatedPeople.Surname;
                people.Phone = updatedPeople.Phone;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating person: " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public ActionResult<People> DeletePeople(int id)
        {
            try
            {
                var person = _context.People.Find(id);

                if (person == null)
                {
                    return NotFound("Person not found.");
                }

                _context.People.Remove(person);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting person: " + ex.Message);
            }
        }
    }
}
