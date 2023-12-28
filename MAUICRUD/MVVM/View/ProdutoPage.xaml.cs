using MauiCrud.MVVM.ViewModels;
using MauiCrud.Service;

namespace MauiCrud.MVVM.View;

public partial class ProdutoPage : ContentPage
{
    public ProdutoPage(IDbService dbService, IErrorService errorService)
    {
        InitializeComponent();
        BindingContext = new ProdutoViewModel(dbService, Navigation, errorService);

    }
}