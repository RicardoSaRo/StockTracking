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
    public class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        ProductDAO dao = new ProductDAO();
        CategoryDAO categorydao = new CategoryDAO();

        public bool Delete(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ProductDetailDTO entity)
        {
            PRODUCT dto = new PRODUCT();
            dto.ProductName = entity.ProductName;
            dto.CategoryID = entity.CategoryID;
            dto.Price = entity.Price;
            return dao.Insert(dto);
        }

        public ProductDTO Select()
        {
            ProductDTO dto = new ProductDTO();
            dto.Products = dao.Select();
            dto.Categories = categorydao.Select();
            return dto;
        }

        public bool Update(ProductDetailDTO entity)
        {
            PRODUCT dto = new PRODUCT();
            dto.ID = entity.ID;
            dto.ProductName = entity.ProductName;
            dto.CategoryID = entity.CategoryID;
            dto.StockAmount = entity.StockAmount;
            dto.Price = entity.Price;
            return dao.Update(dto);
        }
    }
}
