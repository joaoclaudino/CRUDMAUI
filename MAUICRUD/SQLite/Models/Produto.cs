using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.SQLite.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Codigo { get; set; }
        [MaxLength(200), NotNull]
        public string Descricao { get; set; }
        [NotNull]
        public double PesoLiquido { get; set; }
        [NotNull]
        public double PrecoUnitario { get; set; }



    }
}
