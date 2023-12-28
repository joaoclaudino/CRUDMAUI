using MauiCrud.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MauiCrud.Service
{
    public class CepService : ICepService
    {
        public Cep ConsultaCep(string cEp)
        {
            var client = new RestClient($"http://viacep.com.br/ws/{cEp}/json/");
            //client.tim = -1;
            var request = new RestRequest
            {
                Method = Method.Get
            };
            //var request = new RestRequest(Method.Get);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            // Check if the response is not null or empty before deserializing
            if (!string.IsNullOrEmpty(response.Content))
            {
                // Deserialize into a nullable Cep?
                Cep? cep = JsonConvert.DeserializeObject<Cep?>(response.Content);

                // Check if cep is not null before returning
                return cep ?? new Cep(); // Provide a default Cep instance if cep is null
            }
            else
            {
                // Handle the case where the response is null or empty
                // You might want to log an error or take appropriate action.
                // Returning null or throwing an exception are possible options.
                return new Cep(); // Provide a default Cep instance if response is empty
            }
        }
    }
}
