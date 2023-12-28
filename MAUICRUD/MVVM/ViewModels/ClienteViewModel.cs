using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiCrud.MVVM.View;
using MauiCrud.Service;
using MauiCrud.SQLite.Models;
using MauiCrud.Models;
namespace MauiCrud.MVVM.ViewModels
{
    public partial class ClienteViewModel : ObservableObject
    {
        private readonly IErrorService? _errorService;

        [ObservableProperty]
        private List<Cliente>? _clientes;

        [ObservableProperty]
        private Cliente? _clienteAtual;
        [ObservableProperty]
        private bool _isLoading;
        [ObservableProperty]
        private int _code;
        [ObservableProperty]
        private string _nome = String.Empty;
        [ObservableProperty]
        private string _cep = String.Empty;
        [ObservableProperty]
        private string _logradouro = String.Empty;
        [ObservableProperty]
        private string _complemento = String.Empty;
        [ObservableProperty]
        private string _bairro = String.Empty;
        [ObservableProperty]
        private string _cidade = String.Empty;
        [ObservableProperty]
        private string _uf = String.Empty;
        [ObservableProperty]
        private string _ibge = String.Empty;
        public ICommand? AddCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? DisplayCommand { get; set; }
        public ICommand? SairCommand { get; set; }
        public ICommand? CepCommand { get; set; }
        partial void OnClienteAtualChanging(Cliente? value)
        {
            if (value is { Code: > 0 })
            {
                Code = value.Code;
                Nome = value.Nome;
                Cep = value.Cep;
                Logradouro = value.Logradouro;
                Complemento = value.Complemento;
                Bairro = value.Bairro;
                Cidade = value.Cidade;
                Uf = value.Uf;
                Ibge = value.Ibge;
            }
        }
        private void SetClienteAtual()
        {
            ClienteAtual ??= new Cliente();
            ClienteAtual.Code = Code;
            ClienteAtual.Nome = Nome;
            ClienteAtual.Cep = Cep;
            ClienteAtual.Logradouro = Logradouro;
            ClienteAtual.Complemento = Complemento;
            ClienteAtual.Bairro = Bairro;
            ClienteAtual.Cidade = Cidade;
            ClienteAtual.Uf = Uf;
            ClienteAtual.Ibge = Ibge;
        }

        private void ClearFields()
        {
            Code = 0;
            Nome =  string.Empty;
            Cep = string.Empty;
            Logradouro = string.Empty;
            Complemento = string.Empty;
            Bairro = string.Empty;
            Cidade = string.Empty;
            Uf = string.Empty;
            Ibge= string.Empty;
        }

        public ClienteViewModel(IDbService repository, INavigation navigation, IErrorService errorService)
        {
            try
            {
                IsLoading = true;
                _errorService = errorService ?? throw new ArgumentNullException(nameof(errorService));
                ClienteAtual = new Cliente();

                async void Add()
                {
                    try
                    {
                        SetClienteAtual();
                        await repository.InicializeAsync();
                        await repository.AddCliente(ClienteAtual);
                        await Refresh(repository);
                        ClienteAtual = new Cliente();
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
                        SetClienteAtual();
                        await repository.InicializeAsync();
                        await repository.UpdateCliente(ClienteAtual);
                        await Refresh(repository);
                        ClienteAtual = new Cliente();
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
                            var resposta =
                                await Application.Current.MainPage.DisplayAlert("Alerta", "Confirma Exclusão?", "Sim",
                                    "Não");
                            if (resposta)
                            {
                                await repository.DeleteCliente(ClienteAtual);
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

                void Cep()
                {
                    try
                    {
                        ICepService cEpService = new CepService();
                        Cep oCep = cEpService.ConsultaCep(this.Cep);

                        //ClienteAtual = new Cliente()
                        //{
                        //    Code = Code,
                        Nome = ClienteAtual.Nome;
                        Logradouro = oCep.Logradouro;
                        Uf = oCep.Uf;
                        Bairro = oCep.Bairro;
                        Cidade = oCep.Localidade;
                        Ibge = oCep.Ibge;
                        Complemento = oCep.Complemento;
                        this.Cep= oCep.CEp;
                            //};
                    }
                    catch (Exception ex)
                    {
                        errorService.HandleError(ex);
                    }
                }

                CepCommand = new Command(Cep);
                DisplayCommand.Execute(null);
            }
            catch (Exception ex)
            {

                errorService?.HandleError(ex);
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
                Clientes = await repository.GetClientes();
                IsLoading = false;
            }
            catch (Exception ex)
            {
                _errorService?.HandleError(ex);
            }
        }
    }
}
