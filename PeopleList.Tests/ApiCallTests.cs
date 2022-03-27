using NUnit.Framework;
using PeopleList.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleList.Tests
{
    public class ApiCallTests
    {
        private ApiClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new ApiClient();
        }

        [Test]
        public async Task GetAllPeople_ReturnsPeopleData_Count()
        {
            var peopleList = await _client.GetPeopleAsync();
            var peopleListCountShouldBe = 20;
            var passCondition = peopleList.Count == peopleListCountShouldBe;

            Assert.IsTrue(passCondition);
        }

        [Test]
        public async Task SearchAllPeople_ShouldReturnPeopleCountWithSearchTerms()
        {
            var fakeTerm = "kri";
            var peopleList = await _client.SearchPeopleAsync(fakeTerm);
            var searchedPeopleListCountShouldBe = 1;
            var passCondition = peopleList.Count == searchedPeopleListCountShouldBe;

            Assert.IsTrue(passCondition);
        }

        [Test]
        public async Task SearchAllPeople_ShouldReturnPeopleDataWithSearchTerms()
        {
            var fakeTerm = "kri";
            var peopleList = await _client.SearchPeopleAsync(fakeTerm);
            var personUsernameShouldBe = "kristakemp";
            var passCondition = peopleList.Count == 1 && peopleList.FirstOrDefault().UserName == personUsernameShouldBe;

            Assert.IsTrue(passCondition);
        }

        [Test]
        public async Task GetPeopleDetails_ReturnPeopleData()
        {
            var fakePerson = new Person
            {
                FirstName = "Krista",
                LastName = "Kemp",
                MiddleName = null,
                Age = null,
                Gender = "Female",
                UserName = "kristakemp",
                Emails = new List<string> { "Krista@example.com" }
            };

            var personDetails = await _client.GetPersonDetailsAsync("kristakemp");

            Assert.AreEqual(fakePerson, personDetails);
        }
    }
}