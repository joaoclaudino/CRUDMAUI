using CommunityToolkit.Mvvm.ComponentModel;
using MAUICRUD.MVVM.View;
using MAUICRUD.Service;
using MAUICRUD.SQLite.Models;
using System.Windows.Input;

namespace MAUICRUD.MVVM.ViewModels
{
    public partial class ProdutoViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;
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
        public ProdutoViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService)
        {
            try
            {
                IsLoading = true;
                _navigation = navigation;
                _errorService = errorService;
                ProdutoAtual = new Produto();
                AddCommand = new Command(async () =>
                {
                    try
                    {
                        await repositorio.InicializeAsync();
                        await repositorio.AddProduto(ProdutoAtual);
                        await Refresh(repositorio);
                        ProdutoAtual = new Produto();
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });
                UpdateCommand = new Command(async () =>
                {
                    try
                    {
                        await repositorio.InicializeAsync();
                        await repositorio.UpdateProduto(ProdutoAtual);
                        await Refresh(repositorio);
                        ProdutoAtual = new Produto();
                    }
                    catch (Exception ex)
                    {

                        errorService.HandleError(ex);
                    }
                });
                DeleteCommand = new Command(async () =>
                {
                    try
                    {
                        await repositorio.InicializeAsync();
                        if (App.Current is not null && App.Current.MainPage is not null)
                        {
                            var resposta = await App.Current.MainPage.DisplayAlert("Alerta", "Confirma Exclusão?", "Sim", "Não");
                            if (resposta)
                            {
                                await repositorio.DeleteProduto(ProdutoAtual);
                            }
                        }

                        await Refresh(repositorio);
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
        private async Task Refresh(IDBService repositorio)
        {
            try
            {
                IsLoading = true;
                await Task.Delay(3000);
                Produtos = await repositorio.GetProdutos();
                IsLoading = false;
            }
            catch (Exception ex)
            {
                _errorService?.HandleError(ex);
            }
        }
    }
}
