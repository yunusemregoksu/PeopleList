using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleList.Interfaces
{
    public interface IApiClient<T, U>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> SearchAsync(string term);
        Task<T> DetailsAsync(U key);
    }
}
