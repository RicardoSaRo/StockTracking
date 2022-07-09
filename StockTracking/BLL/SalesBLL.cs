using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DTO;
using StockTracking.DAL.DAO;

namespace StockTracking.BLL
{
    public class SalesBLL : IBLL<SalesDetailDTO, SalesDTO>
    {
        SalesDAO dao = new SalesDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        ProductDAO productDAO = new ProductDAO();
        CustomerDAO customerDao = new CustomerDAO();
        public bool Delete(SalesDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(SalesDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SalesDetailDTO entity)
        {
            SALE dto = new SALE();
            dto.ProductID = entity.ProductID;
            dto.CustomerID = entity.CustomerID;
            dto.CategoryID = entity.CategoryID;
            dto.ProductSalesAmount = entity.ProductSalesAmount;
            dto.ProductSalesPrice = entity.ProductSalesPrice;
            dto.SalesDate = entity.SalesDate;
            PRODUCT stockUpdate = new PRODUCT();
            stockUpdate.ID = entity.ProductID;
            stockUpdate.ProductName = entity.ProductName;
            stockUpdate.StockAmount = entity.StockAmount - entity.ProductSalesAmount;
            stockUpdate.Price = entity.ProductSalesPrice;
            productDAO.Update(stockUpdate);
            return dao.Insert(dto);
        }

        public SalesDTO Select()
        {
            SalesDTO dto = new SalesDTO();
            dto.Categories = categoryDAO.Select();
            dto.Products = productDAO.Select();
            dto.Customers = customerDao.Select();
            dto.Sales = dao.Select();
            return dto;

        }

        public bool Update(SalesDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
