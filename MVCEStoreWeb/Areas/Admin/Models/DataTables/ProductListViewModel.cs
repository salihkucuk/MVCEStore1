using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEStoreWeb.Areas.Admin.Models.DataTables
{
    public class ProductListViewModel
    {
        public int Id { get; set; }

        public string Picture { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public bool Enabled { get; set; }

        public string Price { get; set; }

        public string Reviews { get; set; }

        public string UserName { get; set; }

        public string Categories { get; set; }

        public string BrandName { get; set; }

        public string Barcode { get; set; }

        public string ProductCode { get; set; }

       
    }
}
