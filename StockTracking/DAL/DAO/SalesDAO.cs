using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.DAL.DAO
{
    public class SalesDAO : StockContext, IDAO<SALE, SalesDetailDTO>
    {
        public bool Delete(SALE entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SALE entity)
        {
            try
            {
                db.SALES.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SalesDetailDTO> Select()
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
                var list = (from s in db.SALES
                           join c in db.CUSTOMERs on s.CustomerID equals c.ID
                           join k in db.CATEGORies on s.CategoryID equals k.ID
                           join p in db.PRODUCTs on s.ProductID equals p.ID
                           select new
                           {
                               id = s.ID,
                               customerId = c.ID,
                               customerName = c.CustomerName,
                               categoryId = k.ID,
                               categoryName = k.CategoryName,
                               productId = p.ID,
                               productName = p.ProductName,
                               salesAmount = s.ProductSalesAmount,
                               salesPrice = s.ProductSalesPrice,
                               stockAmount = p.StockAmount,
                               salesDate = s.SalesDate
                           }).OrderBy(x => x.salesDate);
                foreach (var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO();
                    dto.ID = item.id;
                    dto.CustomerID = item.customerId;
                    dto.CustomerName = item.customerName;
                    dto.CategoryID = item.categoryId;
                    dto.CategoryName = item.categoryName;
                    dto.ProductID = item.productId;
                    dto.ProductName = item.productName;
                    dto.ProductSalesAmount = item.salesAmount;
                    dto.ProductSalesPrice = item.salesPrice;
                    dto.StockAmount = item.stockAmount;
                    dto.SalesDate = item.salesDate;
                    sales.Add(dto);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SALE entity)
        {
            throw new NotImplementedException();
        }
    }
}
