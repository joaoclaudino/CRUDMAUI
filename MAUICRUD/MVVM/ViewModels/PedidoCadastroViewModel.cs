using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiCrud.MVVM.Models;
using MauiCrud.MVVM.View;
using MauiCrud.Service;
using MauiCrud.SQLite.Models;

namespace MauiCrud.MVVM.ViewModels
{
    public partial class PedidoCadastroViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;
        private readonly IErrorService? _errorService;

        [ObservableProperty]
        private List<PedidoItem>? _pedidoItens = [];

        [ObservableProperty]
        private PedidoVm? _pedidoAtual;

        [ObservableProperty]
        private PedidoItem? _pedidoItemAtual;

        [ObservableProperty]
        private int _codeCliente;
        [ObservableProperty]
        private string _nomeCliente = string.Empty;
        [ObservableProperty]
        private int _codeProduto;
        [ObservableProperty]
        private string _descriptionProduto = string.Empty;
        [ObservableProperty]
        private double _unitPrice;
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
        public PedidoCadastroViewModel(IDbService repository, INavigation navigation, IErrorService errorService)
        {
            try
            {
                _navigation = navigation;
                _errorService = errorService;
                PedidoAtual = new PedidoVm();
                PedidoItemAtual = new PedidoItem();
                PedidoItens = [];
                SairCommand = new Command(async () =>
                {
                    try
                    {
                        MenuPage page = new(repository, errorService);
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
                        Cliente oCliente = await repository.GetClienteById(CodeCliente);
                        CodeCliente = oCliente.Code;
                        NomeCliente = oCliente.Nome;
                        //PedidoAtual = new PedidoVM()
                        //{
                        //    _codeCliente = oCliente.Code,
                        //    NameClient = oCliente.Nome
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
                        Produto oProduto = await repository.GetProdutoById(CodeProduto);
                        CodeProduto = oProduto.Code;
                        DescriptionProduto = oProduto.Description;
                        UnitPrice = oProduto.UnitPrice;
                        Quantidade = 1;
                        PesoLiquido = oProduto.NetWeight;
                        //PedidoAtual = new PedidoVM()
                        //{
                        //    _codeCliente = oCliente.Code,
                        //    NameClient = oCliente.Nome
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
                        PedidoItem oPedidoItem = new()
                        {
                            CodeProduto = this.CodeProduto,
                            ProductDescription = this.DescriptionProduto,
                            UnitPrice = this.UnitPrice,
                            Quantity = this.Quantidade,
                            NetWeight = this.PesoLiquido
                        };
                        PedidoItens.Add(oPedidoItem);
                        //PedidoItens = PedidoItens;
                        //    Produto oProduto = await repository.GetProdutoById(CodeProduto);
                        //    CodeProduto = oProduto.Code;
                        //    ProductDescription = oProduto.Description;
                        //    UnitPrice = oProduto.UnitPrice;
                        //    Quantity = 1;
                        //    //PedidoAtual = new PedidoVM()
                        //    //{
                        //    //    _codeCliente = oCliente.Code,
                        //    //    NameClient = oCliente.Nome
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
                _errorService?.HandleError(ex);
            }
        }

    }
}
