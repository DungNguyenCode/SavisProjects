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
    public class OrderDetailService:IAllinterface<OrderDetails>
    {
        private readonly ContextDb _dbContext;

        public OrderDetailService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(OrderDetails item)
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
            var acc = _dbContext.OrderDetails.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<OrderDetails>> GetAll()
        {
            return await _dbContext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetails> GetById(Guid Id)
        {
            var temp = await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(OrderDetails item)
        {
            var temp = _dbContext.OrderDetails.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.Price = item.Price;
                temp.Quantity = item.Quantity;
                temp.Status = item.Status;
                temp.Id_order = item.Id_order;
                temp.ProductDetail = item.ProductDetail;
                temp.Id_productDetails = item.Id_productDetails;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
