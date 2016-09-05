using System;
using System.Collections.Generic;
using UnibenWeb.Domain.Interfaces.Repository.ReadOnly;

namespace UnibenWeb.Domain.Interfaces.Services
{
    public interface IBaseService : IDisposable
    {
        IEnumerable<T> Pesquisar<T>(int offsetRows, int numRows, string pesquisa, string tabela);
        IEnumerable<T> Pesquisar<T>(string table, int offsetRows, string join, int numRows, string where, string select, string order);
    }
}