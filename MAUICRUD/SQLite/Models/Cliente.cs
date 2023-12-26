using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.SQLite.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [PrimaryKey,AutoIncrement]
        public int Codigo { get; set; }
        [MaxLength(200),NotNull]
        public string Nome { get; set; }
        [MaxLength(10), NotNull]
        public string CEP { get; set; }
        [MaxLength(200), NotNull]
        public string Logradouro { get; set; }
        [MaxLength(200), NotNull]
        public string Complemento { get; set; }
        [MaxLength(100), NotNull]
        public string Bairro { get; set; }
        [MaxLength(100), NotNull]
        public string Cidade { get; set; }
        [MaxLength(100), NotNull]
        public string UF { get; set; }
        [MaxLength(10), NotNull]
        public string IBGE { get; set; }
    }
}
