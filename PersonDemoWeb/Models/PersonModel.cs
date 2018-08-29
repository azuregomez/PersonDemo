using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonDemoWeb.Models
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sport { get; set; }

    }

    public class PersonViewModel
    {
        public PersonModel NewPerson { get; set; }
        public List<PersonModel> People { get; set; }

        public string ServerName { get; set; }
    }
}