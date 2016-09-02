using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.Interfaces.Repository;
using UnibenWeb.Domain.Interfaces.Services;

namespace UnibenWeb.Domain.Services
{
    public class CentroCustoService : ICentroCustoService
    {

        private readonly ICentroCustoRepository _centroCustoRepository;
        //private readonly IPessoaReadOnlyRepository _pessoaReadOnlyRepository;

        public CentroCustoService(ICentroCustoRepository centroCustoRepositoy)
        {
            _centroCustoRepository = centroCustoRepositoy;
        }

        public CentroCusto BuscaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _centroCustoRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public void VincularObjetoContexto(CentroCusto centroCusto)
        {
            _centroCustoRepository.AttachObject(centroCusto);
        }
    }
}
