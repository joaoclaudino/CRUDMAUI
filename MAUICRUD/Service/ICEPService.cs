using MAUICRUD.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.Service
{
    public interface ICEPService
    {
        CEP ConsultaCEP(string cEP);
    }
}
