using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL.DTO;
using StockTracking.DAL;

namespace StockTracking.DAL.DAO
{
    public class ProductDAO : StockContext, IDAO<PRODUCT, ProductDetailDTO>
    {
        public bool Delete(PRODUCT entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PRODUCT entity)
        {
            try
            {
                db.PRODUCTs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductDetailDTO> Select()
        {
            try
            {
                List<ProductDetailDTO> products = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCTs
                           join c in db.CATEGORies on p.CategoryID equals c.ID
                           select new
                           {
                               productID = p.ID,
                               productName = p.ProductName,
                               categoryID = c.ID,
                               categoryName = c.CategoryName,
                               stockAmount = p.StockAmount,
                               price = p.Price
                           }).OrderBy(x => x.productName);
                foreach (var item in list)
                {
                    ProductDetailDTO dto = new ProductDetailDTO();
                    dto.ID = item.productID;
                    dto.ProductName = item.productName;
                    dto.CategoryID = item.categoryID;
                    dto.CategoryName = item.categoryName;
                    dto.StockAmount = item.stockAmount;
                    dto.Price = item.price;
                    products.Add(dto);
                }
                return products;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(PRODUCT entity)
        {
            try
            {
                PRODUCT product = new PRODUCT();
                product = db.PRODUCTs.First(x => x.ID == entity.ID);
                if (entity.CategoryID == 0)
                {
                    product.StockAmount = entity.StockAmount;
                }
                //product.Price = entity.Price;
                //product.ProductName = entity.ProductName;
                //product.CategoryID = entity.CategoryID;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
