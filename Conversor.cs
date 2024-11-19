namespace ConversorDeMoedas
{
    using Newtonsoft.Json; 

    public class Conversor
    {
        private readonly string _apiKey = "225b059c4d58699e115c5419";
        private readonly HttpClient _httpClient;

        public Conversor()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(decimal convertedValue, decimal rate)> ConvertAsync(string fromCurrency, string toCurrency, decimal amount)
        {
            if (fromCurrency == toCurrency)
            {
                throw new ArgumentException("Moeda de origem e destino devem ser diferentes.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("O valor deve ser maior que zero.");
            }

            var url = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{fromCurrency}/{toCurrency}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao se comunicar com a API. Verifique sua conexão e chave.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ConversorResposta>(content);

            if (data == null || data.result != "success")
            {
                throw new Exception($"Falha na conversão. Mensagem da API: {data?.error_type ?? "desconhecida"}");
            }

            var convertedValue = Math.Round(data.conversion_rate * amount, 2);
            return (convertedValue, data.conversion_rate);
        }
    }
}
