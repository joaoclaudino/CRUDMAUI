using SQLite;

namespace MauiCrud.SQLite.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Code { get; set; }
        [MaxLength(200), NotNull]
        public string Description { get; set; } = string.Empty;
        [NotNull]
        public double NetWeight { get; set; }
        [NotNull]
        public double UnitPrice { get; set; }



    }
}
