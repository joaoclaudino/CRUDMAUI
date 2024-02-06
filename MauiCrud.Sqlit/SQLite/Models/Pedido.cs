using SQLite;

namespace MauiCrud.SQLite.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [PrimaryKey, AutoIncrement]
        public long NrOrder { get; set; }
        [NotNull]
        public DateTime EmissionData { get; set; }
        [NotNull]
        public long CodeCliente { get; set; }
        [MaxLength(200), NotNull]
        public string NameCliente { get; set; } = string.Empty;
        [NotNull]
        public double TotalPrice { get; set; }
        [NotNull]
        public double TotalWeight { get; set; }

    }
}

