using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnibenWeb.Application.Interface;

namespace UnibenWeb.UI.MVC.Models
{
    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    public class JQueryDataTableParamModel
    {
        public JQueryDataTableParamModel()
        {
        }
        /// <summary>
        /// Request sequence number sent by DataTable, same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// True if the global filter should be treated as a regular expression for advanced filtering, false if not.
        /// </summary>
        public bool bRegex { get; set; }

        /// <summary>
        /// chave mestre para tabelas de detalhe
        /// </summary>
        public int iMasterKey { get; set; }

        /// <summary>
        ///  	Indicator for if a column (int) is flagged as sortable or not on the client-side
        /// </summary>
        public bool bSortable_0 { get; set; }
        public bool bSortable_1 { get; set; }
        public bool bSortable_2 { get; set; }

        /// <summary>
        /// The value specified by mDataProp for each column. This can be useful for ensuring that the processing of data is independent from the order of the columns.
        /// </summary>
        public string mDataProp_0 { get; set; }
        public string mDataProp_1 { get; set; }
        public string mDataProp_2 { get; set; }

        public List<string> GetCalculatedParams<T>(NameValueCollection searchParams)
        {
            var array = new bool[iColumns];
            var parameters = searchParams.Keys.Cast<string>().ToDictionary(key => key, val => searchParams[val]);
            var colunas = parameters["sColumns"].Split(',');
            // parameters["sEcho"]
            var individualSearch = "";
            var globalSearch = "";
            var order = "";

            //var sortColumnIndex = Convert.ToInt32(parameters["iSortCol_0"]);
            //var sorDirection = ();

            var results = new List<string> { };

            for (int i = 0; i < iColumns; i++)
            {
                // GLOBAL SEARCH 
                if (Convert.ToBoolean(parameters["bSearchable_" + i.ToString()]) && (parameters["sSearch"] != ""))
                {
                    if (globalSearch != "") { globalSearch += " OR "; }
                    globalSearch += "(" + colunas[i] + " like '%" + parameters["sSearch"] + "%')";
                };

                // INDIVIDUAL SEARCHES 

                if (Convert.ToBoolean(parameters["bSearchable_" + i.ToString()]) && (parameters["sSearch_" + i.ToString()] != ""))
                {
                    if (individualSearch != "") { individualSearch += " AND "; }
                    individualSearch += "(" + colunas[i] + " like '%" + parameters["sSearch_" + i.ToString()] + "%')";
                };
            }

            for (int i = 0; i < Convert.ToInt32(parameters["iSortingCols"]); i++)
            {
                var sortColumnIndex = Convert.ToInt32(parameters["iSortCol_" + i.ToString()]);
                // INDIVIDUAL SORTS 
                if (Convert.ToBoolean(parameters["bSortable_" + sortColumnIndex.ToString()])) // && (sortColumnIndex == i)
                {
                    if (order != "") { order += " , "; }
                    order += colunas[sortColumnIndex] + " " + parameters["sSortDir_" + i.ToString()];
                };
            }

                results.Add(globalSearch);
            if (globalSearch != "" && individualSearch != "")
            { results.Add(" AND " + individualSearch); }
            else
            { results.Add(individualSearch); }
            if (order == "") { order = "1"; };
            results.Add(order);
            return results;

            //var dynExp = System.Linq.Dynamic.DynamicExpression.ParseLambda(new[] { linqParam }, null, exp);
            //var obj = new { };
            //Convert.ChangeType(obj, typeof(T)); precisar implementar iconvertible
            //var result = dynExp.Compile().DynamicInvoke((T)obj);
            //return (null);
        }
    }
}