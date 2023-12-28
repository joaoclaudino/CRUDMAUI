using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiCrud.MVVM.View;
using MauiCrud.Service;

namespace MauiCrud.MVVM.ViewModels
{
    public class MenuViewModel : ObservableObject
    {
        private readonly IErrorService? _errorService;
        public ICommand? ProdutoCommand { get; set; }
        public ICommand? ClienteCommand { get; set; }
        public ICommand? PedidoCommand { get; set; }

        public MenuViewModel(IDbService repository, INavigation navigation, IErrorService errorService)
        {
            try
            {
                var navigation1 = navigation;
                _errorService = errorService;

                ProdutoCommand = new Command(async () =>
                {
                    try
                    {
                        await navigation1.PushModalAsync(new ProdutoPage(repository, errorService));
                    }
                    catch (Exception ex)
                    {

                        _errorService.HandleError(ex);
                    }
                });
                ClienteCommand = new Command(async () =>
                {
                    try
                    {
                        await navigation1.PushModalAsync(new ClientePage(repository, errorService));
                    }
                    catch (Exception ex)
                    {

                        _errorService.HandleError(ex);
                    }
                });
                PedidoCommand = new Command(async () =>
                {
                    try
                    {
                        await navigation1.PushModalAsync(new PedidoConsultaPage(repository, errorService));
                    }
                    catch (Exception ex)
                    {

                        _errorService.HandleError(ex);
                    }
                });

            }
            catch (Exception ex)
            {

                _errorService?.HandleError(ex);
            }
        }
    }
}
