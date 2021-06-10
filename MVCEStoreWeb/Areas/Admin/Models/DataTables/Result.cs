using System.Collections.Generic;

namespace MVCEStoreWeb.Areas.Admin.Models.DataTables
{
    public class Result<T>
    {
        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public List<T> data { get; set; }
    }
}
