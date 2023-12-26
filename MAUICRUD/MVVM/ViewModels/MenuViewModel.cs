using CommunityToolkit.Mvvm.ComponentModel;
using MAUICRUD.MVVM.View;
using MAUICRUD.Service;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
namespace MAUICRUD.MVVM.ViewModels
{
    public class MenuViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        public ICommand ProdutoCommand { get; set; }
        public ICommand ClienteCommand { get; set; }
        public ICommand PedidoCommand { get; set; }
        public ICommand CEPCommand { get; set; }
        
        public MenuViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService)
        {
            _navigation = navigation;

            ProdutoCommand = new Command(async () =>
            {                
                await _navigation.PushModalAsync(new ProdutoPage(repositorio, errorService)); 
            });
            ClienteCommand = new Command(async () =>
            {
                await _navigation.PushModalAsync(new ClientePage(repositorio, errorService));
            });
            PedidoCommand = new Command(async () =>
            {
                await _navigation.PushModalAsync(new PedidoConsultaPage(repositorio, errorService));
            });
        }
    }
}
