using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string NomeCliente { get; set; }
        [NotNull]
        public double PrecoTotal { get; set; }
        [NotNull]
        public double PesoTotal { get; set; }

    }
}

