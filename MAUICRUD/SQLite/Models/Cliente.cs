using SQLite;

namespace MAUICRUD.SQLite.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Codigo { get; set; }
        [MaxLength(200), NotNull]
        public string Nome { get; set; } = string.Empty;
        [MaxLength(10), NotNull]
        public string CEP { get; set; } = string.Empty;
        [MaxLength(200), NotNull]
        public string Logradouro { get; set; } = string.Empty;
        [MaxLength(200), NotNull]
        public string Complemento { get; set; } = string.Empty;
        [MaxLength(100), NotNull]
        public string Bairro { get; set; } = string.Empty;
        [MaxLength(100), NotNull]
        public string Cidade { get; set; } = string.Empty;
        [MaxLength(100), NotNull]
        public string UF { get; set; } = string.Empty;
        [MaxLength(10), NotNull]
        public string IBGE { get; set; } = string.Empty;
    }
}
