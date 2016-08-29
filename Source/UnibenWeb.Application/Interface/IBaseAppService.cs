using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UnibenWeb.Application.Interface
{
    public interface IBaseAppService : IDisposable
    { 
        IEnumerable<T> Pesquisar<T>(int offsetRows, int numRows, string pesquisa, string tabela);
        IEnumerable<T> Pesquisar<T>(string table, int offsetRows, string join, int numRows, string where, string select, string order);
        SelectList ListasDeSelecao<T>(string id, string descricao, string tabela, string pesquisa);
        SelectList ListasDeSelecao<T>(string table, int offsetRows, string join, int numRows, string where, string select, string order, string id, string descricao);
    }
}