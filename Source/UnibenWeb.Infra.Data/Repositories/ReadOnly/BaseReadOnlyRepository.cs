using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.Interfaces.Repository.ReadOnly;

namespace UnibenWeb.Infra.Data.Repositories.ReadOnly
{
    public class BaseReadOnlyRepository : IBaseReadOnlyRepository
    {
        public IDbConnection Connection => new SqlConnection(ConfigurationManager.ConnectionStrings["UnibenConnection"].ConnectionString);

        public IEnumerable<T> Pesquisar<T>(int offsetRows, int numRows, string pesquisa, string tabela)
        {
            using (var cn = Connection)
            {
                cn.Open();
                string sql;
                if (string.IsNullOrEmpty(pesquisa))
                {
                    sql = @"SELECT * FROM {0} tb ORDER BY 1";
                    sql = string.Format(sql, tabela);
                }
                else
                {
                    sql = @"SELECT * FROM {0} tb WHERE ({1}) ORDER BY 1";
                    sql = string.Format(sql, tabela, pesquisa);
                }

                if (numRows > 0)
                {
                    sql += " OFFSET @pOffset ROWS FETCH NEXT @pRows ROWS ONLY ";
                }

                var result = cn.Query<T>(sql, new { pPesquisa = pesquisa, pOffset = offsetRows, pRows = numRows });
                return result;
            }
        }

        public IEnumerable<T> Pesquisar<T>(string table, int offsetRows, string join, int numRows, string where, string select, string order)
        {
            using (var cn = Connection)
            {
                cn.Open();
                string sql = @"SELECT * FROM (";

                sql += " SELECT {0} FROM {1} AS TB {2}";
                sql = string.Format(sql, select, table, join);

                if (string.IsNullOrEmpty(where))
                {
                    sql += ") TMP  ORDER BY {0} ";
                    sql = string.Format(sql, order);
                }
                else
                {
                    sql += ") TMP WHERE {0} ORDER BY {1} ";
                    sql = string.Format(sql, where, order);
                }

                if (numRows > 0)
                {
                    sql += " OFFSET @pOffset ROWS FETCH NEXT @pRows ROWS ONLY ";
                }

                var result = cn.Query<T>(sql, new { pOffset = offsetRows, pRows = numRows });
                return result;

            }
        }


    }
}
