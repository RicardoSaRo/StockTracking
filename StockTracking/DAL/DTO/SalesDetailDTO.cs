using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DTO
{
    public class SalesDetailDTO
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int ProductSalesAmount { get; set; }

        public int ProductSalesPrice { get; set; }

        public int StockAmount { get; set; }

        public DateTime SalesDate { get; set; }
    }
}
