namespace UnibenWeb.UI.MVC.Models
{
    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    public class JQueryDataTableParamModel
    {
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


    }
}