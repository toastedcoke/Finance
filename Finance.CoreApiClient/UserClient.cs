using Finance.Entity.Model;
using Finance.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.CoreApiClient
{
    public partial class ApiClient
    {
        public async Task<List<NpvDTO>> GetUsers1()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "api/WeatherForecast"));
            return await GetAsync<List<NpvDTO>>(requestUrl);
        }

        public async Task<IEnumerable<WeatherForecast>>> GetUsers()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "api/WeatherForecast"));
            return await GetAsync<IEnumerable<WeatherForecast>>(requestUrl);
        }

        public async Task<MessageDTO<NpvDTO>> SaveUser(NpvDTO model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "User/SaveUser"));
            return await PostAsync<NpvDTO>(requestUrl, model);
        }
    }
}