using CommunityToolkit.Mvvm.ComponentModel;
using MAUICRUD.Service;
using MAUICRUD.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using MAUICRUD.MVVM.View;

namespace MAUICRUD.MVVM.ViewModels
{
    public partial class ProdutoViewModel : ObservableObject
    {
        private readonly INavigation _navigation;

        [ObservableProperty]
        private List<Produto> _produtos;

        [ObservableProperty]
        private Produto _produtoAtual;

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DisplayCommand { get; set; }
        public ICommand SairCommand { get; set; }
        public ProdutoViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService)
        {
            _navigation = navigation;
            ProdutoAtual = new Produto();
            AddCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();
                await repositorio.AddProduto(ProdutoAtual);
                await Refresh(repositorio);
                ProdutoAtual = new Produto();
            });
            UpdateCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();
                await repositorio.UpdateProduto(ProdutoAtual);
                await Refresh(repositorio);
                ProdutoAtual = new Produto();
            });
            DeleteCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();
                var resposta = await App.Current.MainPage.DisplayAlert("Alerta","Confirma Exclusão?","Sim","Não");
                if (resposta)
                {
                    await repositorio.DeleteProduto(ProdutoAtual);
                }
                await Refresh(repositorio);
            });
            DisplayCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();                
                await Refresh(repositorio);
            });
            SairCommand = new Command(async () =>
            {
                MenuPage page = new MenuPage(repositorio, errorService);
                await _navigation.PushModalAsync(page);
            });
            DisplayCommand.Execute(null);
        }
        private async Task Refresh(IDBService repositorio)
        {
            Produtos = await repositorio.GetProdutos();
        }
    }
}
