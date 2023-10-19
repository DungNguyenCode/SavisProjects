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
    public class CartServices :IAllinterface<Cart>
    {
        private readonly ContextDb _dbContext;

        public CartServices(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Cart item)
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
            var acc = _dbContext.Carts.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await _dbContext.Carts.ToListAsync();
        }

        public async Task<bool> Update(Cart item)
        {
            var temp = _dbContext.Carts.FirstOrDefault(a => a.Id == item.Id);

            if (temp != null)
            {
              temp.Create_at = DateTime.Now;
              
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
