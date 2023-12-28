using SQLite;

namespace MauiCrud.SQLite.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [PrimaryKey, AutoIncrement]
        public int NrPedido { get; set; }
        [NotNull]
        public DateTime EmissionData { get; set; }
        [NotNull]
        public int CodeCliente { get; set; }
        [MaxLength(200), NotNull]
        public string NameClient { get; set; } = string.Empty;
        [NotNull]
        public double TotalPrice { get; set; }
        [NotNull]
        public double TotalWeight { get; set; }

    }
}

