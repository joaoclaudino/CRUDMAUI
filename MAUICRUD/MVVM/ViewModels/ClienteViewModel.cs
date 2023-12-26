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
    public partial class ClienteViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IErrorService _errorService;

        [ObservableProperty]
        private List<Cliente> _clientes;

        [ObservableProperty]
        private Cliente _clienteAtual;

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DisplayCommand { get; set; }
        public ICommand SairCommand { get; set; }
        public ICommand CEPCommand { get; set; }
        public ClienteViewModel(IDBService repositorio, INavigation navigation, IErrorService errorService)
        {
            _errorService = errorService ?? throw new ArgumentNullException(nameof(errorService));
            _navigation = navigation;
            ClienteAtual = new Cliente();
            AddCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();
                await repositorio.AddCliente(ClienteAtual);
                await Refresh(repositorio);
                ClienteAtual = new Cliente();
            });
            UpdateCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();
                await repositorio.UpdateCliente(ClienteAtual);
                await Refresh(repositorio);
                ClienteAtual = new Cliente();
            });
            DeleteCommand = new Command(async () =>
            {
                await repositorio.InicializeAsync();
                var resposta = await App.Current.MainPage.DisplayAlert("Alerta","Confirma Exclusão?","Sim","Não");
                if (resposta)
                {
                    await repositorio.DeleteCliente(ClienteAtual);
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
            CEPCommand = new Command(async () =>
            {
                try
                {
                    ICEPService cEPService = new CEPService();
                    CEP oCEP = cEPService.ConsultaCEP(ClienteAtual.CEP);      

                    ClienteAtual = new Cliente() 
                    { Codigo=ClienteAtual.Codigo, Nome=ClienteAtual.Nome, Logradouro = oCEP.logradouro
                    ,UF = oCEP.uf, Bairro = oCEP.bairro, Cidade = oCEP.localidade, IBGE = oCEP.ibge
                    , Complemento = oCEP.complemento,CEP=oCEP.cep };
                }
                catch (Exception ex)
                {

                    _errorService.HandleError(ex);
                }

            });
            DisplayCommand.Execute(null);
        }
        private async Task Refresh(IDBService repositorio)
        {
            Clientes = await repositorio.GetClientes();
        }
    }
}
