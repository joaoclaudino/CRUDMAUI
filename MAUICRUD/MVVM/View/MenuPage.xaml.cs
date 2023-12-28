using MauiCrud.MVVM.ViewModels;
using MauiCrud.Service;

namespace MauiCrud.MVVM.View;

public partial class MenuPage : ContentPage
{

    public MenuPage(IDbService dbService, IErrorService errorService)
    {
        InitializeComponent();
        BindingContext = new MenuViewModel(dbService, Navigation, errorService);
    }
}