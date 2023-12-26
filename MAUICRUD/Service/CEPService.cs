using MAUICRUD.SQLite.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CEP cep = JsonConvert.DeserializeObject<CEP>(response.Content);
            return cep;
        }
    }
}
