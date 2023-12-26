using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.SQLite.Models
{
    public class CEP
    {
        public required string cep { get; set; }
        public required string logradouro { get; set; }
        public required string complemento { get; set; }
        public required string bairro { get; set; }
        public required string localidade { get; set; }
        public required string uf { get; set; }
        public required string ibge { get; set; }
        public required string gia { get; set; }
        public required string ddd { get; set; }
        public required string siafi { get; set; }
    }

}
