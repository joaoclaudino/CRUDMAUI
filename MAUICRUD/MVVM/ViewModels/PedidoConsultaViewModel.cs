using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiCrud.MVVM.View;
using MauiCrud.Service;
using MauiCrud.SQLite.Models;

namespace MauiCrud.MVVM.ViewModels
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
        public PedidoConsultaViewModel(IDbService repository, INavigation navigation, IErrorService errorService)
        {
            try
            {
                _navigation = navigation;
                _errorService = errorService;
                PedidoAtual = new Pedido();
                async void Updt()
                {
                    try
                    {
                        if (PedidoAtual.NrOrder>0)
                        {
                            PedidoCadastroPage page = new(repository, errorService,PedidoAtual.NrOrder);
                            await _navigation.PushModalAsync(page);
                        }
                        //PedidoCadastroPage page = new(repository, errorService);
                        //await _navigation.PushModalAsync(page);
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }
                UpdateCommand = new Command(Updt);
                async void Add()
                {
                    try
                    {
                        PedidoCadastroPage page = new(repository, errorService);
                        await _navigation.PushModalAsync(page);
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }
                AddCommand = new Command(Add);

                async void Sair()
                {
                    try
                    {
                        MenuPage page = new(repository, errorService);
                        await navigation.PushModalAsync(page);
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }
                SairCommand = new Command(Sair);

                async void Display()
                {
                    try
                    {
                        await repository.InicializeAsync();
                        await Refresh(repository);
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }
                DisplayCommand = new Command(Display);
                DisplayCommand.Execute(null);
            }
            catch (Exception ex)
            {

                errorService.HandleError(ex);
            }
        }
        private async Task Refresh(IDbService repositorio)
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
