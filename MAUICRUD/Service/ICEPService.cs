using MAUICRUD.SQLite.Models;

namespace MAUICRUD.Service
{
    public interface ICEPService
    {
        CEP ConsultaCEP(string cEP);
    }
}
