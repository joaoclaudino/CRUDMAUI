using SQLite;

namespace MAUICRUD.SQLite.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Codigo { get; set; }
        [MaxLength(200), NotNull]
        public string Descricao { get; set; } = string.Empty;
        [NotNull]
        public double PesoLiquido { get; set; }
        [NotNull]
        public double PrecoUnitario { get; set; }



    }
}
