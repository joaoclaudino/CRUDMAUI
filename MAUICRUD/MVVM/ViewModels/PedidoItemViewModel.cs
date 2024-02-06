using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiCrud.MVVM.ViewModels
{

    public partial class PedidoItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private long _codePedidoItem;
        [ObservableProperty]
        private long _nrOrder;
        [ObservableProperty]
        private int _lineId;
        [ObservableProperty]
        private long _codeProduto;
        [ObservableProperty] 
        private string _productDescription = String.Empty;
        [ObservableProperty]
        private double _quantity;
        [ObservableProperty]
        private double _unitPrice;
        [ObservableProperty]
        private double _netWeight;
        [ObservableProperty]
        private double _totalPrice;
        [ObservableProperty]
        private double _totalWeight;

    }
}
