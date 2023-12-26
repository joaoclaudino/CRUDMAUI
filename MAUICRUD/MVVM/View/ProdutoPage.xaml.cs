using MAUICRUD.Service;
using MAUICRUD.MVVM.ViewModels;

namespace MAUICRUD.MVVM.View;

public partial class ProdutoPage : ContentPage
{
	private readonly IDBService _DBService;

    public ProdutoPage(IDBService DBService, IErrorService errorService)
	{
		InitializeComponent();
        _DBService = DBService;
        BindingContext = new ProdutoViewModel(_DBService,Navigation, errorService);

    }
}