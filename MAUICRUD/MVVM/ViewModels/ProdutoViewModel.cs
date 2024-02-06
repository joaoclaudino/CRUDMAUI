using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiCrud.MVVM.View;
using MauiCrud.Service;
using MauiCrud.SQLite.Models;

namespace MauiCrud.MVVM.ViewModels
{
    public partial class ProdutoViewModel : ObservableObject
    {
        private readonly IErrorService? _errorService;
        [ObservableProperty]
        private List<Produto>? _produtos;
        [ObservableProperty]
        private Produto? _produtoAtual;
        public ICommand? AddCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? DisplayCommand { get; set; }
        public ICommand? SairCommand { get; set; }
        [ObservableProperty]
        private bool _isLoading;
        [ObservableProperty]
        private long _code;
        [ObservableProperty] 
        private string _description = String.Empty;
        [ObservableProperty]
        private double _netWeight;
        [ObservableProperty]
        private double _unitPrice;
        partial void OnProdutoAtualChanging(Produto? value)
        {
            if (value is { Code: > 0 })
            {
                Code = value.Code;
                Description = value.Description;
                UnitPrice = value.UnitPrice;
                NetWeight = value.NetWeight;
            }
        }
        private void SetProdutoAtual()
        {
            ProdutoAtual ??= new Produto();
            ProdutoAtual.Code = Code;
            ProdutoAtual.Description = Description;
            ProdutoAtual.UnitPrice = UnitPrice;
            ProdutoAtual.NetWeight = NetWeight;
        }

        private void ClearFields()
        {
            Code = 0;
            Description = string.Empty;
            UnitPrice = 0;
            NetWeight = 0;
        }
        public ProdutoViewModel(IDbService repository, INavigation navigation, IErrorService errorService)
        {
            try
            {
                IsLoading = true;
                _errorService = errorService;
                ProdutoAtual = new Produto();
                //AddCommand = new Command(execute: async () => await ExecuteAddCommandAsync(repository));
                async void Add()
                {
                    try
                    {
                        SetProdutoAtual();
                        await repository.InicializeAsync();
                        await repository.AddProduto(ProdutoAtual);
                        await Refresh(repository);
                        ProdutoAtual = new Produto();
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }

                AddCommand = new Command(Add);

                async void Upt()
                {
                    try
                    {
                        SetProdutoAtual();
                        await repository.InicializeAsync();
                        await repository.UpdateProduto(ProdutoAtual);
                        await Refresh(repository);
                        ProdutoAtual = new Produto();
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }
                UpdateCommand = new Command(Upt);
                async void Del()
                {
                    try
                    {
                        await repository.InicializeAsync();
                        if (Application.Current is not null && Application.Current.MainPage is not null)
                        {
                            var resposta = await Application.Current.MainPage.DisplayAlert("Alerta", "Confirma Exclusão?", "Sim", "Não");
                            if (resposta)
                            {
                                await repository.DeleteProduto(ProdutoAtual);
                                ProdutoAtual = null;
                            }
                        }

                        await Refresh(repository);
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }
                DeleteCommand = new Command(Del);
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
                DisplayCommand.Execute(null);
            }
            catch (Exception ex)
            {
                errorService.HandleError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private async Task Refresh(IDbService repository)
        {
            try
            {
                IsLoading = true;
                await Task.Delay(3000);
                Produtos = await repository.GetProdutos();
                IsLoading = false;
            }
            catch (Exception ex)
            {
                _errorService?.HandleError(ex);
            }
        }
    }
}
