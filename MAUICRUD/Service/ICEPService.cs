using MauiCrud.SQLite.Models;

namespace MauiCrud.Service
{
    public interface ICepService
    {
        Cep ConsultaCep(string cEp);
    }
}
