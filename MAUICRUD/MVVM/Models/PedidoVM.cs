using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiCrud.MVVM.Models
{

    public partial class PedidoVm : ObservableObject
    {
        public int NrPedido { get; set; }
        public DateTime DataEmissao { get; set; }

        [ObservableProperty]
        private int _codeCliente;

        [ObservableProperty]
        private string _nomeCliente = string.Empty;
        public double PrecoTotal { get; set; }
        public double PesoTotal { get; set; }
    }
}
