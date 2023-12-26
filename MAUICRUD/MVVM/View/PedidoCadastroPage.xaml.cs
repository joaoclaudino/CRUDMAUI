using MAUICRUD.MVVM.ViewModels;
using MAUICRUD.Service;

namespace MAUICRUD.MVVM.View;

public partial class PedidoCadastroPage : ContentPage
{
    private readonly IDBService _DBService;
    public PedidoCadastroPage(IDBService DBService, IErrorService errorService)
	{
		InitializeComponent();
        _DBService = DBService;
        BindingContext = new PedidoCadastroViewModel(_DBService, Navigation, errorService);
    }
}