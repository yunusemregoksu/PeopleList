using PeopleList;
using PeopleList.Util;
using System.Collections;
using System.Reflection;

class Program
{
    readonly static PersonApiClient apiClient = new();

    static async Task Main(string[] args)
    {
        // Display title as the C# console calculator app.
        Console.WriteLine("Console People List\r");
        Console.WriteLine("------------------------\n");

        await MainMenu();
        Console.ReadKey();
    }

    static async Task MainMenu()
    {
        // Ask the user to choose an option.
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\ta - List all of the people");
        Console.WriteLine("\ts - Search specific username, name or surname");
        Console.WriteLine("\td - Details of the person (will be asking to username later if this one is choosen)");
        Console.WriteLine("\te - Exit the application.");
        Console.Write("Your option? ");

        var input = Console.ReadLine();
        // Use a switch statement to do selected task.
        switch (input)
        {
            case "a":
                await GetPeopleListAsync();
                break;
            case "s":
                await SearchPeopleListAsync();
                break;
            case "d":
                await PersonDetailAsync();
                break;
            case "e":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Please select a valid option.");
                break;
        }

        // call main menu again after all job is done.
        await MainMenu();
    }

    private static async Task PersonDetailAsync()
    {
        try
        {
            Console.WriteLine("Enter a username for details:");
            var input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Please enter a username");
            }

            var person = await apiClient.DetailsAsync(input ?? "");
            var personData = new List<KeyValuePair<string, string>>();

            Type personType = person.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(personType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                string propValue = prop.GetValue(person, null)?.ToString() ?? "";

                if (prop.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)) && prop.PropertyType != typeof(string))
                {
                    int index = 1;
                    List<string> valueList = new();

                    foreach (var listItem in prop.GetValue(person, null) as IEnumerable)
                    {
                        valueList.Add($"{index}: {listItem}");
                        index++;
                    }

                    propValue = string.Join(" | ", valueList);
                }

                personData.Add(new KeyValuePair<string, string>(prop.Name, propValue));
            }

            Console.WriteLine(personData.ToStringTable(new[] { "Key", "Value" }, a => a.Key, a => a.Value));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task SearchPeopleListAsync()
    {
        try
        {
            Console.WriteLine("Enter search terms (at least 3 characters):");

            var input = Console.ReadLine();

            if (input?.Length < 3)
            {
                Console.WriteLine("Please write at least 3 characters:");

                await SearchPeopleListAsync();
            }

            var filteredPeopleList = await apiClient.SearchAsync(input ?? "");

            Console.WriteLine(filteredPeopleList.ToStringTable(new[] { "UserName", "Name", "Surname" }, a => a.UserName, a => a.FirstName, a => a.LastName));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task GetPeopleListAsync()
    {
        try
        {
            var peopleList = await apiClient.GetAllAsync();

            Console.WriteLine(peopleList.ToStringTable(new[] { "UserName", "Name", "Surname" }, a => a.UserName, a => a.FirstName, a => a.LastName));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}