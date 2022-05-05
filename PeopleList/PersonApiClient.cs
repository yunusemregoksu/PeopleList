using PeopleList.Interfaces;
using PeopleList.Model;
using PeopleList.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleList
{
    public class PersonApiClient : IApiClient<Person, string>
    {
        public async Task<List<Person>> GetAllAsync()
        {
            var people = await ApiHelper.DoGetRequest<OData<List<Person>>>("https://services.odata.org/TripPinRESTierService/People");

            return people.Value;
        }

        public async Task<List<Person>> SearchAsync(string term)
        {
            var people = await ApiHelper.DoGetRequest<OData<List<Person>>>($"https://services.odata.org/TripPinRESTierService/People?$filter=contains(tolower(FirstName),'{term}') or contains(tolower(LastName), '{term.ToLower()}')");

            return people.Value;
        }

        public async Task<Person> DetailsAsync(string userName)
        {
            var person = await ApiHelper.DoGetRequest<Person>($"https://services.odata.org/TripPinRESTierService/People('{userName.ToLower()}')");

            return person;
        }
    }
}
