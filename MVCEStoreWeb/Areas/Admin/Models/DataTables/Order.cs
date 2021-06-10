﻿namespace MVCEStoreWeb.Areas.Admin.Models.DataTables
{   
    public enum OrderDir
    {
        ASC,
        DESC
    }
    public class Order
    {
        public int Column { get; set; }

        public OrderDir Dir { get; set; }
    }
}
