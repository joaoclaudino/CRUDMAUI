using MAUICRUD.SQLite.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MAUICRUD.Service
{
    public class CEPService : ICEPService
    {
        public CEP ConsultaCEP(string cEP)
        {
            var client = new RestClient(string.Format("http://viacep.com.br/ws/{0}/json/", cEP));
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
                // Deserialize into a nullable CEP?
                CEP? cep = JsonConvert.DeserializeObject<CEP?>(response.Content);

                // Check if cep is not null before returning
                return cep ?? new CEP(); // Provide a default CEP instance if cep is null
            }
            else
            {
                // Handle the case where the response is null or empty
                // You might want to log an error or take appropriate action.
                // Returning null or throwing an exception are possible options.
                return new CEP(); // Provide a default CEP instance if response is empty
            }
        }
    }
}
