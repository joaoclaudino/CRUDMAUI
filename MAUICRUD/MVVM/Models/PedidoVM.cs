using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUICRUD.MVVM.Models
{

    public partial class PedidoVM : ObservableObject
    {
        public int NrPedido { get; set; }
        public DateTime DataEmissao { get; set; }

        [ObservableProperty]
        public int _codigoCliente;

        [ObservableProperty]
        private string _nomeCliente = string.Empty;
        public double PrecoTotal { get; set; }
        public double PesoTotal { get; set; }
    }
}
