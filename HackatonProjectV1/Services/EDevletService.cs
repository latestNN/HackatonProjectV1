namespace HackatonProjectV1.Services
{
    public class EDevletService
    {
        private readonly HttpClient _httpClient;

        public EDevletService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> DogrulaAsyn(string barcode)
        {
            var url = $"https://edevlet.emrgncr.com/belge?barcode={barcode}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return "Hata: API başarısız döndü";

            return await response.Content.ReadAsStringAsync();
        }
    }
}
