using SQLite;

namespace MauiCrud.SQLite.Models
{
    [Table("PedidoItens")]
    public class PedidoItem
    {
        [PrimaryKey, AutoIncrement]
        public int CodePedidoItem { get; set; }
        [NotNull]
        public int NrPedido { get; set; }
        [NotNull]
        public int CodeProduto { get; set; }
        [MaxLength(200), NotNull]
        public string ProductDescription { get; set; } = string.Empty;
        [NotNull]
        public double Quantity { get; set; }
        [NotNull]
        public double UnitPrice { get; set; }
        [NotNull]
        public double NetWeight { get; set; }
        [NotNull]
        public double TotalPrice { get; set; }
        [NotNull]
        public double TotalWeight { get; set; }

    }
}
