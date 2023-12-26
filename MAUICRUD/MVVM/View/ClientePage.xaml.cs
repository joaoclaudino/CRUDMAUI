using MAUICRUD.MVVM.ViewModels;
using MAUICRUD.Service;

namespace MAUICRUD.MVVM.View;

public partial class ClientePage : ContentPage
{
    private readonly IDBService _DBService;
    //private 
    public ClientePage(IDBService DBService,IErrorService errorService)
	{
		InitializeComponent();
        _DBService = DBService;
        BindingContext = new ClienteViewModel(_DBService, Navigation, errorService);
    }
}