using Data.ContextDbSavis;
using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class ProductDetailService:IAllinterface<ProductDetail>
    {
        private readonly ContextDb _dbContext;

        public ProductDetailService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(ProductDetail item)
        {
            if (item != null)
            {
                await _dbContext.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var acc = _dbContext.ProductDetails.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProductDetail>> GetAll()
        {
            return await _dbContext.ProductDetails.ToListAsync();
        }

        public async Task<bool> Update(ProductDetail item)
        {
            var temp = _dbContext.ProductDetails.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
               temp.Price = item.Price;
                temp.Id_Product = item.Id_Product;
                temp.Id_Material = item.Id_Material;
                temp.Id_Brand = item.Id_Brand;
                temp.Status = item.Status;
                temp.Description = item.Description;
                temp.Id_Color = item.Id_Color;
                temp.Id_Product = item.Id_Product;
                temp.Id_Size = item.Id_Size;
                temp.Last_modified_date = DateTime.Now;
                temp.Gender = item.Gender;
                temp.Description = item.Description;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
