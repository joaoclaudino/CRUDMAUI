using CommunityToolkit.Mvvm.ComponentModel;
using MAUICRUD.MVVM.Models;
using MAUICRUD.MVVM.View;
using MAUICRUD.Service;
using MAUICRUD.SQLite.Models;
using System.Windows.Input;

namespace MAUICRUD.MVVM.ViewModels
{
    public partial class PedidoCadastroViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;
        private readonly IErrorService? _errorService;

        [ObservableProperty]
        private List<PedidoItem>? _pedidoItens;

        [ObservableProperty]
        private PedidoVM? _pedidoAtual;

        [ObservableProperty]
        private PedidoItem? _pedidoItemAtual;

        [ObservableProperty]
        private int _codigoCliente;
        [ObservableProperty]
        private string _nomeCliente = string.Empty;
        [ObservableProperty]
        private int _codigoProduto;
        [ObservableProperty]
        private string _descricaoProduto = string.Empty;
        [ObservableProperty]
        private double _precoUnitario;
        [ObservableProperty]
        private double _quantidade;
        [ObservableProperty]
        private double _pesoLiquido;
        [ObservableProperty]
        private bool _isRefreshing;
        public ICommand? SairCommand { get; set; }
        public ICommand? SalvarCadastroCommand { get; set; }
        public ICommand? ConsultarClienteCommand { get; set; }
        public ICommand? ConsultarProdutoCommand { get; set; }
        public ICommand? SalvarProdutoCommand { get; set; }
        public ICommand? NovoProdutoCommand { get; set; }
        public PedidoCadastroViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService)
        {
            try
            {
                _navigation = navigation;
                _errorService = errorService;
                PedidoAtual = new PedidoVM();
                PedidoItens = new List<PedidoItem>();
                PedidoItemAtual = new PedidoItem();
                SairCommand = new Command(async () =>
                {
                    try
                    {
                        MenuPage page = new MenuPage(repositorio, errorService);
                        await _navigation.PushModalAsync(page);
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });
                ConsultarClienteCommand = new Command(async () =>
                {
                    try
                    {
                        Cliente oCliente = await repositorio.GetClienteById(CodigoCliente);
                        CodigoCliente = oCliente.Codigo;
                        NomeCliente = oCliente.Nome;
                        //PedidoAtual = new PedidoVM()
                        //{
                        //    CodigoCliente = oCliente.Codigo,
                        //    NomeCliente = oCliente.Nome
                        //};
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });
                ConsultarProdutoCommand = new Command(async () =>
                {
                    try
                    {
                        Produto oProduto = await repositorio.GetProdutoById(CodigoProduto);
                        CodigoProduto = oProduto.Codigo;
                        DescricaoProduto = oProduto.Descricao;
                        PrecoUnitario = oProduto.PrecoUnitario;
                        Quantidade = 1;
                        PesoLiquido = oProduto.PesoLiquido;
                        //PedidoAtual = new PedidoVM()
                        //{
                        //    CodigoCliente = oCliente.Codigo,
                        //    NomeCliente = oCliente.Nome
                        //};
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });

                SalvarProdutoCommand = new Command(() =>
                {
                    try
                    {
                        IsRefreshing = true;
                        PedidoItens.Add(new PedidoItem()
                        {
                            CodigoProduto = this.CodigoProduto,
                            DescricaoProduto = this.DescricaoProduto,
                            PrecoUnitario = this.PrecoUnitario,
                            Quantidade = this.Quantidade,
                            PesoLiquido = this.PesoLiquido
                        });
                        PedidoItens = PedidoItens;
                        //    Produto oProduto = await repositorio.GetProdutoById(CodigoProduto);
                        //    CodigoProduto = oProduto.Codigo;
                        //    DescricaoProduto = oProduto.Descricao;
                        //    PrecoUnitario = oProduto.PrecoUnitario;
                        //    Quantidade = 1;
                        //    //PedidoAtual = new PedidoVM()
                        //    //{
                        //    //    CodigoCliente = oCliente.Codigo,
                        //    //    NomeCliente = oCliente.Nome
                        //    //};
                        IsRefreshing = false;
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                if (_errorService is not null)
                {
                    _errorService.HandleError(ex);
                }
            }
        }

    }
}
