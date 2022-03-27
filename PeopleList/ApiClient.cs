using PeopleList.Model;
using PeopleList.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleList
{
    public class ApiClient
    {
        public async Task<List<Person>> GetPeopleAsync()
        {
            var people = await ApiHelper.DoGetRequest<OData<List<Person>>>("https://services.odata.org/TripPinRESTierService/People");

            return people.Value;
        }

        public async Task<List<Person>> SearchPeopleAsync(string term)
        {
            var people = await ApiHelper.DoGetRequest<OData<List<Person>>>($"https://services.odata.org/TripPinRESTierService/People?$filter=contains(tolower(FirstName),'{term}') or contains(tolower(LastName), '{term.ToLower()}')");

            return people.Value;
        }

        public async Task<Person> GetPersonDetailsAsync(string userName)
        {
            var person = await ApiHelper.DoGetRequest<Person>($"https://services.odata.org/TripPinRESTierService/People('{userName.ToLower()}')");

            return person;
        }
    }
}
