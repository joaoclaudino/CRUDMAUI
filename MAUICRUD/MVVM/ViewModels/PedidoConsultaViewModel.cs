using CommunityToolkit.Mvvm.ComponentModel;
using MAUICRUD.SQLite.Models;
using MAUICRUD.MVVM.View;
using MAUICRUD.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUICRUD.MVVM.ViewModels
{
    public partial class PedidoConsultaViewModel : ObservableObject
    {
        private readonly INavigation _navigation;

        [ObservableProperty]
        private List<Pedido> _pedidos;

        [ObservableProperty]
        private Pedido _pedidoAtual;

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DisplayCommand { get; set; }
        public ICommand SairCommand { get; set; }
        public PedidoConsultaViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService) {
            _navigation = navigation;
            PedidoAtual = new Pedido();
            AddCommand = new Command(async () =>
            {
                PedidoCadastroPage page = new PedidoCadastroPage(repositorio, errorService);
                await _navigation.PushModalAsync(page);
            });
            SairCommand = new Command(async () =>
            {
                MenuPage page = new MenuPage(repositorio, errorService);
                await _navigation.PushModalAsync(page);
            });
            DisplayCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();
                await Refresh(repositorio);
            });
            DisplayCommand.Execute(null);
        }
        private async Task Refresh(IDBService repositorio)
        {
            Pedidos = await repositorio.GetPedidos();
        }
    }
}
