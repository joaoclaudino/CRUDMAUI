using SQLite;

namespace MAUICRUD.SQLite.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [PrimaryKey, AutoIncrement]
        public int NrPedido { get; set; }
        [NotNull]
        public DateTime DataEmissao { get; set; }
        [NotNull]
        public int CodigoCliente { get; set; }
        [MaxLength(200), NotNull]
        public string NomeCliente { get; set; } = string.Empty;
        [NotNull]
        public double PrecoTotal { get; set; }
        [NotNull]
        public double PesoTotal { get; set; }

    }
}

