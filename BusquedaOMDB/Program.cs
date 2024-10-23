using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dominio;

namespace BusquedaOMDB
{
    class Program
    {
        private static readonly string apiKey = "1504665a";
        private static readonly string baseUrl = "http://www.omdbapi.com/";

        static async Task Main(string[] args)
        {
            Console.Write("Ingrese el titulo de la serie: ");
            string title = Console.ReadLine();

            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("El título no puede estar vacío.");
                return;
            }

            var serie = await GetSerieDataAsync(title);

            if (serie != null)
            {
                Console.WriteLine($"Titulo: {serie.Title}");
                Console.WriteLine($"Director: {serie.Director}");
                Console.WriteLine($"Actores: {serie.Actors}");
                      Console.WriteLine($"Genero: {serie.Genre}");
            }
            else
            {
                Console.WriteLine("No se encontró la serie.");
            }
        }

        static async Task<Serie> GetSerieDataAsync(string title)
        {
            using HttpClient client = new HttpClient();
            string url = $"{baseUrl}?t={title}&apikey={apiKey}&type=series";

            try
            {
                return await client.GetFromJsonAsync<Serie>(url);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en obtener datos de la API: {e.Message}");
                return null;
            }
        }
    }
}
