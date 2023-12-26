using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.MVVM.Models
{

    public partial class PedidoVM : ObservableObject
    {
        public int NrPedido { get; set; }
        public DateTime DataEmissao { get; set; }

        [ObservableProperty]
        public int _codigoCliente;

        [ObservableProperty]
        private string _nomeCliente;
        public double PrecoTotal { get; set; }
        public double PesoTotal { get; set; }
    }
}
