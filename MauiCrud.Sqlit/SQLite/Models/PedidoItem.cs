using SQLite;

namespace MauiCrud.SQLite.Models
{
    [Table("PedidoItens")]
    public class PedidoItem
    {
        [PrimaryKey, AutoIncrement]
        public long CodePedidoItem { get; set; }
        [NotNull]
        public long NrOrder { get; set; }
        [NotNull]
        public int LineId { get; set; }
        [NotNull]
        public long CodeProduto { get; set; }
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
