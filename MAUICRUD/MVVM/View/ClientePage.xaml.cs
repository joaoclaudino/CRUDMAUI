using MauiCrud.MVVM.ViewModels;
using MauiCrud.Service;

namespace MauiCrud.MVVM.View;

public partial class ClientePage : ContentPage
{
    //private 
    public ClientePage(IDbService dbService, IErrorService errorService)
    {
        InitializeComponent();
        BindingContext = new ClienteViewModel(dbService, Navigation, errorService);
    }
}