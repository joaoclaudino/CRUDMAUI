using SQLite;

namespace MauiCrud.SQLite.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public long Code { get; set; }
        [MaxLength(200), NotNull]
        public string Nome { get; set; } = string.Empty;
        [MaxLength(10), NotNull]
        public string Cep { get; set; } = string.Empty;
        [MaxLength(200), NotNull]
        public string Logradouro { get; set; } = string.Empty;
        [MaxLength(200), NotNull]
        public string Complemento { get; set; } = string.Empty;
        [MaxLength(100), NotNull]
        public string Bairro { get; set; } = string.Empty;
        [MaxLength(100), NotNull]
        public string Cidade { get; set; } = string.Empty;
        [MaxLength(100), NotNull]
        public string Uf { get; set; } = string.Empty;
        [MaxLength(10), NotNull]
        public string Ibge { get; set; } = string.Empty;
    }
}
