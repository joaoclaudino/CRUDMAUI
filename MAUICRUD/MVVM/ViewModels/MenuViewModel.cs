using CommunityToolkit.Mvvm.ComponentModel;
using MAUICRUD.MVVM.View;
using MAUICRUD.Service;
using System.Windows.Input;
namespace MAUICRUD.MVVM.ViewModels
{
    public class MenuViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;
        private readonly IErrorService? _errorService;
        public ICommand? ProdutoCommand { get; set; }
        public ICommand? ClienteCommand { get; set; }
        public ICommand? PedidoCommand { get; set; }
        public ICommand? CEPCommand { get; set; }

        public MenuViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService)
        {
            try
            {
                _navigation = navigation;
                _errorService = errorService;

                ProdutoCommand = new Command(async () =>
                {
                    try
                    {
                        await _navigation.PushModalAsync(new ProdutoPage(repositorio, errorService));
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });
                ClienteCommand = new Command(async () =>
                {
                    try
                    {
                        await _navigation.PushModalAsync(new ClientePage(repositorio, errorService));
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });
                PedidoCommand = new Command(async () =>
                {
                    try
                    {
                        await _navigation.PushModalAsync(new PedidoConsultaPage(repositorio, errorService));
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });

            }
            catch (Exception ex)
            {

                errorService.HandleError(ex);
            }
        }
    }
}
