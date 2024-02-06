using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiCrud.MVVM.View;
using MauiCrud.Service;
using MauiCrud.SQLite.Models;
using System.Collections.ObjectModel;

namespace MauiCrud.MVVM.ViewModels
{
    public partial class PedidoCadastroViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;
        private readonly IErrorService? _errorService;

        [ObservableProperty]
        private ObservableCollection<PedidoItemViewModel>? _pedidoItens;

        [ObservableProperty]
        private Pedido? _pedidoAtual;

        [ObservableProperty]
        private PedidoItemViewModel? _pedidoItemAtual;

        [ObservableProperty]
        private long _codeCliente;
        [ObservableProperty]
        private string _nameCliente = string.Empty;
        [ObservableProperty]
        private long _codeProduto;
        [ObservableProperty]
        private string _productDescription = string.Empty;
        [ObservableProperty]
        private double _unitPrice;
        [ObservableProperty]
        private double _quantity;
        [ObservableProperty]
        private double _netWeight;
        [ObservableProperty]
        private bool _isRefreshing;
        [ObservableProperty]
        private long _nrOrder;
        [ObservableProperty]
        private int _lineId;
        [ObservableProperty]
        private DateTime _emissionData;
        [ObservableProperty]
        private double _totalPrice;
        [ObservableProperty]
        private double _totalWeight;
        public ICommand? SairCommand { get; set; }
        public ICommand? SalvarCadastroCommand { get; set; }
        public ICommand? ConsultarClienteCommand { get; set; }
        public ICommand? ConsultarProdutoCommand { get; set; }
        public ICommand? SalvarProdutoCommand { get; set; }
        public ICommand? NovoProdutoCommand { get; set; }
        private void SetPedidoAtual()
        {
            PedidoAtual = new()
            {
                NrOrder = NrOrder,
                EmissionData = EmissionData,
                CodeCliente = CodeCliente,
                NameCliente = NameCliente,
                TotalPrice = TotalPrice,
                //PedidoAtual.UnitPrice = UnitPrice;
                TotalWeight = TotalWeight
            };
        }

        partial void OnPedidoItemAtualChanged(global::MauiCrud.MVVM.ViewModels.PedidoItemViewModel? value)
        {
            if (value != null)
            {
                //NrOrder = value.NrOrder;
                UnitPrice = value.UnitPrice;
                ProductDescription = value.ProductDescription;
                Quantity = value.Quantity;
                NetWeight = value.NetWeight;
                //CodeClienteodePedidoItem = value.CodePedidoItem;
                CodeProduto = value.CodeProduto;
                //TotalPrice = value.TotalPrice;
                //TotalWeight = value.TotalWeight;
                LineId=value.LineId;
            }
        }
        partial void OnQuantityChanged(double value)
        {
            TotalPrice = Quantity * UnitPrice;
        }

        public PedidoCadastroViewModel(IDbService repository, INavigation navigation, IErrorService errorService, long nrPedido)
        {
            try
            {
                _navigation = navigation;
                _errorService = errorService;
                PedidoAtual = new Pedido();
                PedidoItemAtual = new PedidoItemViewModel();
                PedidoItens = [];

                async void CarregarPedido()
                {
                    try
                    {
                        if (nrPedido > 0)
                        {
                            PedidoAtual = await repository.GetPedidoById(nrPedido);
                            NrOrder = PedidoAtual.NrOrder;
                            EmissionData = PedidoAtual.EmissionData;
                            CodeCliente = PedidoAtual.CodeCliente;
                            NameCliente = PedidoAtual.NameCliente;
                            TotalPrice = PedidoAtual.TotalPrice;
                            TotalWeight = PedidoAtual.TotalWeight;


                            List<PedidoItem> lstPedidoItem=await repository.GetPedidoItemByNrOrder(NrOrder);
                            foreach (PedidoItem item in lstPedidoItem)
                            {
                                PedidoItens.Add(new PedidoItemViewModel()
                                {
                                    NrOrder= item.NrOrder,
                                    UnitPrice = item.UnitPrice,
                                    ProductDescription = item.ProductDescription,
                                    Quantity = item.Quantity,
                                    NetWeight = item.NetWeight,
                                    CodePedidoItem = item.CodePedidoItem,
                                    CodeProduto = item.CodeProduto,
                                    TotalPrice = item.TotalPrice,
                                    TotalWeight = item.TotalWeight,
                                    LineId = item.LineId
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }

                CarregarPedido();
                async void Save()
                {
                    try
                    {
                        SetPedidoAtual();
                        await repository.InicializeAsync();
                        List<PedidoItem> lstPedidoItem = [];
                        foreach (PedidoItemViewModel pedidoItemViewModel in PedidoItens)
                        {
                            lstPedidoItem.Add(new PedidoItem()
                            {
                                NrOrder=pedidoItemViewModel.NrOrder,
                                UnitPrice = pedidoItemViewModel.UnitPrice,
                                ProductDescription = pedidoItemViewModel.ProductDescription,
                                Quantity = pedidoItemViewModel.Quantity,
                                NetWeight = pedidoItemViewModel.NetWeight,
                                TotalWeight = pedidoItemViewModel.TotalWeight,
                                TotalPrice = pedidoItemViewModel.TotalPrice,
                                CodePedidoItem=pedidoItemViewModel.CodePedidoItem,
                                CodeProduto = pedidoItemViewModel.CodeProduto,
                                LineId = pedidoItemViewModel.LineId
                                
                            });
                        }
                        //if (PedidoAtual.NrOrder>0)
                        //{
                            await repository.SalvarPedidoCompleto(PedidoAtual, lstPedidoItem);
                            //await repository.UpdatePedido(PedidoAtual);
                        //}
                        //else
                        //{
                            //await repository.AddPedido(PedidoAtual);
                        //}
                        //var i =await repository.AddPedido(PedidoAtual);
                        //await repository.
                        //await Refresh(repository);
                        //PedidoAtual = new Pedido();
                        //ClearFields();
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }
                SalvarCadastroCommand = new Command(Save);
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

                ConsultarClienteCommand = new Command(async () =>
                {
                    try
                    {
                        Cliente? oCliente = await repository.GetClienteById(CodeCliente);
                        if (oCliente is null)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"Cliente não Encontrado", "OK");
                        }
                        else
                        {
                            CodeCliente = oCliente.Code;
                            NameCliente = oCliente.Nome;
                        }

                        //PedidoAtual = new PedidoVM()
                        //{
                        //    _codeCliente = oCliente.Code,
                        //    NameCliente = oCliente.Nome
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
                        ProductDescription = oProduto.Description;
                        UnitPrice = oProduto.UnitPrice;
                        Quantity = 1;
                        NetWeight = oProduto.NetWeight;
                        //PedidoAtual = new PedidoVM()
                        //{
                        //    _codeCliente = oCliente.Code,
                        //    NameCliente = oCliente.Nome
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
                        if (this.LineId>0)
                        {
                            PedidoItemViewModel oPedidoItem = new()
                            {
                                CodeProduto = this.CodeProduto,
                                ProductDescription = this.ProductDescription,
                                UnitPrice = this.UnitPrice,
                                Quantity = this.Quantity,
                                NetWeight = this.NetWeight,
                                TotalPrice = this.UnitPrice * this.Quantity,
                                TotalWeight = this.NetWeight * this.Quantity,
                                LineId = this.LineId,
                                CodePedidoItem = PedidoItemAtual.CodePedidoItem

                            };
                            PedidoItens[PedidoItens.IndexOf(PedidoItens.First(a => a.LineId == this.LineId))] =
                                oPedidoItem;
                            //
                        }
                        else
                        {
                            PedidoItemViewModel oPedidoItem = new()
                            {
                                CodeProduto = this.CodeProduto,
                                ProductDescription = this.ProductDescription,
                                UnitPrice = this.UnitPrice,
                                Quantity = this.Quantity,
                                NetWeight = this.NetWeight,
                                TotalPrice = this.UnitPrice * this.Quantity,
                                TotalWeight = this.NetWeight * this.Quantity,
                                LineId = PedidoItens.Count + 1

                            };
                            PedidoItens.Add(oPedidoItem);
                        }

                        CleanProductFields();
                        CalcTotals();
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

        private void CleanProductFields()
        {
            this.CodeProduto = 0;
            this.ProductDescription = string.Empty;
            this.UnitPrice = 0;
            this.Quantity = 0;
            this.NetWeight = 0;
            this.LineId = 0;
        }

        private void CalcTotals()
        {
            double dTotalPrice = 0;
            double dTotalWeight = 0;
            foreach (PedidoItemViewModel item in PedidoItens)
            {
                dTotalPrice = dTotalPrice + item.TotalPrice;
                dTotalWeight = dTotalWeight + item.TotalWeight;
            }

            TotalPrice = dTotalPrice;
            TotalWeight = dTotalWeight;
        }
    }
}
