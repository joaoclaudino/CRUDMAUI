using CommunityToolkit.Mvvm.ComponentModel;
using MAUICRUD.MVVM.View;
using MAUICRUD.Service;
using MAUICRUD.SQLite.Models;
using System.Windows.Input;

namespace MAUICRUD.MVVM.ViewModels
{
    public partial class PedidoConsultaViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;
        private readonly IErrorService? _errorService;

        [ObservableProperty]
        private List<Pedido>? _pedidos;

        [ObservableProperty]
        private Pedido? _pedidoAtual;

        public ICommand? AddCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? DisplayCommand { get; set; }
        public ICommand? SairCommand { get; set; }
        public PedidoConsultaViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService)
        {
            try
            {
                _navigation = navigation;
                _errorService = errorService;
                PedidoAtual = new Pedido();
                AddCommand = new Command(async () =>
                {
                    try
                    {
                        PedidoCadastroPage page = new(repositorio, errorService);
                        await _navigation.PushModalAsync(page);
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }

                });
                SairCommand = new Command(async () =>
                {
                    try
                    {
                        MenuPage page = new(repositorio, errorService);
                        await _navigation.PushModalAsync(page);
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }

                });
                DisplayCommand = new Command(async () =>
                {
                    try
                    {
                        await repositorio.InicializeAsync();
                        await Refresh(repositorio);
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }

                });
                DisplayCommand.Execute(null);
            }
            catch (Exception ex)
            {

                errorService.HandleError(ex);
            }
        }
        private async Task Refresh(IDBService repositorio)
        {
            try
            {
                Pedidos = await repositorio.GetPedidos();
            }
            catch (Exception ex)
            {
                _errorService?.HandleError(ex);
            }

        }
    }
}
