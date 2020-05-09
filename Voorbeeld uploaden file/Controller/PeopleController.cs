using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorld.Controller
{
    public class Person
    {
        private static int idCounter = 0;

        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person(string fn, string ln)
        {
            Id = idCounter++;
            FirstName = fn;
            LastName = ln;
        }
    }

    public class PeopleController : ApiController
    {
        public static List<Person> People = new List<Person>();

        static PeopleController()
        {
            People.Add(new Controller.Person("John", "Smith"));
            People.Add(new Controller.Person("Peter", "Pan"));
            People.Add(new Controller.Person("Marilyn", "Monroe"));
        }

        public IEnumerable<Person> Get()
        {
            return People;
        }
        public IEnumerable<Person> GetPeople()
        {
            return People;
        }

        public Person GetPerson(int id)
        {
            return People.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet]
        public Person Person(int id)
        {
            return People.FirstOrDefault(x => x.Id == id);
        }

        /* JSON request
        {
	        "FirstName": "Kate",
	        "LastName": "Anderson"
        }
        */
        public void Post([FromBody] Person p)
        {
            People.Add(p);
        }

        [HttpPost]
        public void AddPerson([FromBody] Person p)
        {
            People.Add(p);
        }

        public void Put(int id, [FromBody] Person p)
        {
            var person = People.FirstOrDefault(x => x.Id == id);

            if(person != null)
            {
                person.FirstName = p.FirstName;
                person.LastName = p.LastName;
            }
        }
    }
}
