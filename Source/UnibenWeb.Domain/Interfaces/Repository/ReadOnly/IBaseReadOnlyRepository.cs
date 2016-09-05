using System.Collections.Generic;

namespace UnibenWeb.Domain.Interfaces.Repository.ReadOnly
{
    public interface IBaseReadOnlyRepository
    {
        IEnumerable<T> Pesquisar<T>(int offsetRows, int numRows, string pesquisa, string tabela);
        IEnumerable<T> Pesquisar<T>(string table, int offsetRows, string join, int numRows, string where, string select, string order);
    }
}