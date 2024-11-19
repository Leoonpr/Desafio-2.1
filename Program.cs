using ConversorDeMoedas;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var converter = new Conversor();

        while (true)
        {
            try
            {
                Console.Write("Moeda origem: ");
                string? fromCurrencyInput = Console.ReadLine()?.Trim().ToUpper();
                if (string.IsNullOrEmpty(fromCurrencyInput)) break;

                string fromCurrency = fromCurrencyInput;

                Console.Write("Moeda destino: ");
                string? toCurrencyInput = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrEmpty(toCurrencyInput))
                {
                    Console.WriteLine("A moeda de destino não pode ser nula.");
                    continue;
                }
                string toCurrency = toCurrencyInput;

                Console.Write("Valor: ");
                if (!decimal.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
                {
                    Console.WriteLine("O valor deve ser maior que zero.");
                    continue;
                }

                var (convertedValue, rate) = await converter.ConvertAsync(fromCurrency, toCurrency, amount);

                Console.WriteLine($"{fromCurrency} {amount:F1} => {toCurrency} {convertedValue:F2}");
                Console.WriteLine($"Taxa: {rate:F6}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        Console.WriteLine("Programa finalizado.");
    }
}
