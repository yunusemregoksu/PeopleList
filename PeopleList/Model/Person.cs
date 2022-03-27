using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleList.Model
{
    public class Person
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public List<string> Emails { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            if (object.ReferenceEquals(this, obj)) { return true; }

            var person = obj as Person;
            bool emailComparison;

            if (person.Emails == null && Emails == null)
            {
                emailComparison = true;
            }
            else
            {
                foreach (var email in Emails)
                {
                    person.Emails.Contains(email);
                }

                emailComparison = person.Emails.Count == Emails.Count;
            }

            return UserName == person.UserName
                && FirstName == person.FirstName
                && MiddleName == person.MiddleName
                && LastName == person.LastName
                && Gender == person.Gender
                && Age == person.Age
                && emailComparison;
        }
    }
}
