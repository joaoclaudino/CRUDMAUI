using MauiCrud.MVVM.ViewModels;
using MauiCrud.Service;

namespace MauiCrud.MVVM.View;

public partial class PedidoCadastroPage : ContentPage
{
    public PedidoCadastroPage(IDbService dbService, IErrorService errorService)
    {
        InitializeComponent();
        BindingContext = new PedidoCadastroViewModel(dbService, Navigation, errorService);
    }
}