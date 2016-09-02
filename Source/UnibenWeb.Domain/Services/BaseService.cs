using System;
using System.Collections.Generic;
using UnibenWeb.Domain.Interfaces.Repository;
using UnibenWeb.Domain.Interfaces.Repository.ReadOnly;
using UnibenWeb.Domain.Interfaces.Services;

namespace UnibenWeb.Domain.Services
{
    public class BaseService : IBaseService
    {
        private readonly IBaseReadOnlyRepository _baseReadOnlyRepository;

        public BaseService(IBaseReadOnlyRepository baseReadOnlyRepository)
        {
            _baseReadOnlyRepository = baseReadOnlyRepository;
        }

        public void Dispose()
        {
            // dispose repo
        }

        public IEnumerable<T> Pesquisar<T>(int offsetRows, int numRows, string pesquisa, string tabela)
        {
            return _baseReadOnlyRepository.Pesquisar<T>(offsetRows, numRows, pesquisa, tabela);

        }

        public IEnumerable<T> Pesquisar<T>(string table, int offsetRows, string join, int numRows, string where, string select, string order)
        {
            return _baseReadOnlyRepository.Pesquisar<T>(table, offsetRows, join, numRows, where, select, order);
        }

    }
}