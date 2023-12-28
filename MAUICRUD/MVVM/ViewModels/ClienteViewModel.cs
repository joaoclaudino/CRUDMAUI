using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiCrud.MVVM.View;
using MauiCrud.Service;
using MauiCrud.SQLite.Models;

namespace MauiCrud.MVVM.ViewModels
{
    public partial class ClienteViewModel : ObservableObject
    {
        private readonly IErrorService? _errorService;

        [ObservableProperty]
        private List<Cliente>? _clientes;

        [ObservableProperty]
        private Cliente? _clienteAtual;

        public ICommand? AddCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? DisplayCommand { get; set; }
        public ICommand? SairCommand { get; set; }
        public ICommand? CepCommand { get; set; }
        public ClienteViewModel(IDbService repository, INavigation navigation, IErrorService errorService)
        {
            try
            {

                _errorService = errorService ?? throw new ArgumentNullException(nameof(errorService));
                ClienteAtual = new Cliente();
                AddCommand = new Command(async () =>
                {
                    await repository.InicializeAsync();
                    await repository.AddCliente(ClienteAtual);
                    await Refresh(repository);
                    ClienteAtual = new Cliente();
                });
                UpdateCommand = new Command(execute: async () =>
                {
                    await repository.InicializeAsync();
                    await repository.UpdateCliente(ClienteAtual);
                    await Refresh(repository);
                    ClienteAtual = new Cliente();
                });
                DeleteCommand = new Command(async () =>
                {
                    await repository.InicializeAsync();
                    if (App.Current is not null && App.Current.MainPage is not null)
                    {
                        var resposta = await App.Current.MainPage.DisplayAlert("Alerta", "Confirma Exclusão?", "Sim", "Não");
                        if (resposta)
                        {
                            await repository.DeleteCliente(ClienteAtual);
                        }
                    }

                    await Refresh(repository);
                });
                DisplayCommand = new Command(async () =>
                {
                    await repository.InicializeAsync();
                    await Refresh(repository);
                });
                SairCommand = new Command(async () =>
                {
                    MenuPage page = new(repository, errorService);
                    await navigation.PushModalAsync(page);
                });
                CepCommand = new Command(() =>
                {
                    try
                    {
                        CepService cEpService = new();
                        Cep oCep = cEpService.ConsultaCep(ClienteAtual.Cep);

                        ClienteAtual = new Cliente()
                        {
                            Code = ClienteAtual.Code,
                            Nome = ClienteAtual.Nome,
                            Logradouro = oCep.Logradouro
                        ,
                            Uf = oCep.Uf,
                            Bairro = oCep.Bairro,
                            Cidade = oCep.Localidade,
                            Ibge = oCep.Ibge
                        ,
                            Complemento = oCep.Complemento,
                            Cep = oCep.CEp
                        };
                    }
                    catch (Exception ex)
                    {

                        _errorService.HandleError(ex);
                    }

                });
                DisplayCommand.Execute(null);
            }
            catch (Exception ex)
            {

                errorService?.HandleError(ex);
            }
        }
        private async Task Refresh(IDbService repository)
        {
            try
            {
                Clientes = await repository.GetClientes();
            }
            catch (Exception ex)
            {
                _errorService?.HandleError(ex);
            }
        }
    }
}
