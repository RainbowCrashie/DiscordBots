using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordNetPlus.DataServices
{
    public class Derpibooru : IDisposable
    {
        #region Fields and Properties

        public static Uri Adress { get; } = new Uri("https://derpibooru.org/");
        private readonly HttpClient _httpClient = new HttpClient();

        #endregion

        #region Ctor
        public Derpibooru()
        {
            InitializeHttpClient();
        }
        #endregion

        #region Methods
        private void InitializeHttpClient()
        {
            _httpClient.BaseAddress = Adress;
        }

        public async Task<string> Request(string path)
        {
            var response = await _httpClient.GetAsync(path);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}