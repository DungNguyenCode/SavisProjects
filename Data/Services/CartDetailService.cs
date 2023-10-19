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
    public class CartDetailService : IAllinterface<CartDetails>
    {
        private readonly ContextDb _dbContext;

        public CartDetailService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(CartDetails item)
        {
            if (item != null)
            {
                item.CreateDate = DateTime.Now;
                await _dbContext.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var acc = _dbContext.CartDetails.FirstOrDefault(a => a.ID == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<CartDetails>> GetAll()
        {
            return await _dbContext.CartDetails.ToListAsync();
        }

        public async Task<bool> Update(CartDetails item)
        {
            var temp = _dbContext.CartDetails.FirstOrDefault(a => a.ID == item.ID);

            if (temp != null)
            {
                temp.Id_productdetails = item.Id_productdetails;
                temp.Id_Cart = item.Id_Cart;
                temp.Quantity = item.Quantity;
                temp.Price = item.Price;
                temp.Status = item.Status;
                temp.Last_modified_date = item.Last_modified_date;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
