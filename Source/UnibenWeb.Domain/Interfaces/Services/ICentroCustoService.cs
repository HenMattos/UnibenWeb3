using System;
using UnibenWeb.Domain.Entities;

namespace UnibenWeb.Domain.Interfaces.Services
{
    public interface ICentroCustoService : IDisposable
    {
        CentroCusto BuscaPorId(int id);
        void VincularObjetoContexto(CentroCusto centroCusto);
    }
}
