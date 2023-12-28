using MauiCrud.MVVM.ViewModels;
using MauiCrud.Service;

namespace MauiCrud.MVVM.View;

public partial class PedidoConsultaPage : ContentPage
{
    public PedidoConsultaPage(IDbService dbService, IErrorService errorService)
    {
        InitializeComponent();
        BindingContext = new PedidoConsultaViewModel(dbService, Navigation, errorService);
    }
}