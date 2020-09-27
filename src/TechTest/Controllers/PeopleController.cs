using System;
using Microsoft.AspNetCore.Mvc;
using TechTest.Repositories;
using TechTest.Repositories.Models;

namespace TechTest.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        public PeopleController(IPersonRepository personRepository)
        {
            this.PersonRepository = personRepository;
        }

        private IPersonRepository PersonRepository { get; }

        [HttpGet]
        public IActionResult GetAll()
        {
            // TODO: Step 1
            //
            // Implement a JSON endpoint that returns the full list
            // of people from the PeopleRepository. If there are zero
            // people returned from PeopleRepository then an empty
            // JSON array should be returned.

            //get list of people from personRepo.
            var peopleList = this.PersonRepository.GetAll();
            //if list has values, return the list.
            if(peopleList != null)
            {
                return this.Ok(peopleList);
            }
            else
            {
                //if null return empty array.
                return this.Ok(new object[0]);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // TODO: Step 2
            //
            // Implement a JSON endpoint that returns a single person
            // from the PeopleRepository based on the id parameter.
            // If null is returned from the PeopleRepository with
            // the supplied id then a NotFound should be returned.

            //get person from id.
            var person = this.PersonRepository.Get(id);
            
            if (person != null)
            {
                return this.Ok(person);
            }
            else
            {
                //if null return notFound.
                return this.NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PersonUpdate personUpdate)
        {
            // TODO: Step 3
            //
            // Implement an endpoint that receives a JSON object to
            // update a person using the PeopleRepository based on
            // the id parameter. Once the person has been successfully
            // updated, the person should be returned from the endpoint.
            // If null is returned from the PeopleRepository then a
            // NotFound should be returned.

            //get person from id.
            var person = this.PersonRepository.Get(id);

            if (person != null)
            {
                //check if relevant (changable on the application) values are altered, if so - amend the person's details.
                if (person.Authorised != personUpdate.Authorised) { person.Authorised = personUpdate.Authorised; }
                if (person.Enabled != personUpdate.Enabled) { person.Enabled = personUpdate.Enabled; }
                if (person.Colours != personUpdate.Colours) { person.Colours = personUpdate.Colours; }
                //Use update function on person from specified id.
                var updatedPerson = this.PersonRepository.Update(person);

                if (updatedPerson != null)
                {
                    return this.Ok(updatedPerson);
                }
                else
                {
                    return this.NotFound();
                }
            }
            else
            {
                //if null return notFound.
                return this.NotFound();
            }

        }
    }
}