using MAUICRUD.MVVM.ViewModels;
using MAUICRUD.Service;

namespace MAUICRUD.MVVM.View;

public partial class MenuPage : ContentPage
{
    private readonly IDBService _DBService;
    private readonly IErrorService _ErrorService;
    public MenuPage(IDBService DBService, IErrorService errorService)
	{
		InitializeComponent();
        _DBService = DBService;
        _ErrorService = errorService;
        BindingContext = new MenuViewModel(_DBService, Navigation,errorService);
       // NavigationManager.PushAsync(repositorio);

        //Navigation.
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        ProdutoPage page = new ProdutoPage(_DBService, _ErrorService);
        Navigation.PushModalAsync(page);
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        ClientePage page = new ClientePage(_DBService, _ErrorService);
        Navigation.PushModalAsync(page);
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
      
    }
}