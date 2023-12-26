using MAUICRUD.MVVM.ViewModels;
using MAUICRUD.Service;

namespace MAUICRUD.MVVM.View;

public partial class PedidoConsultaPage : ContentPage
{
    private readonly IDBService _DBService;
    public PedidoConsultaPage(IDBService DBService, IErrorService errorService)
    {
        InitializeComponent();
        _DBService = DBService;
        BindingContext = new PedidoConsultaViewModel(_DBService, Navigation, errorService);
    }
}