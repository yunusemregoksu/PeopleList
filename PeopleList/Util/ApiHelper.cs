using Newtonsoft.Json;
using PeopleList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleList.Util
{
    public static class ApiHelper
    {
        public async static Task<T> DoGetRequest<T>(string url)
        {
            using var client = new HttpClient();
            var json = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<T>(json);

            if (data == null)
            {
                throw new Exception("No available data found");
            }

            return data;
        }
    }
}
