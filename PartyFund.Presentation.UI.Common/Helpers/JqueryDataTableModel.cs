using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common.Helpers
{
  public  class JqueryDataTableModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
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
        /// Comma separated list of Table Listing Category
        /// </summary>
        public string sDisplayCategory { get; set; }

        /// <summary>
        /// Name of the job # to be searched server side parameter
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// Search user name parameter for user search other tickets information server side paramerter from datatable
        /// </summary>
        public string sUser { get; set; }
        /// <summary>
        /// For displaying active tickets
        /// </summary>
        public string sActive { get; set; }

        public int ticketID { get; set; }

        public int SectionId { get; set; }
    }
}
