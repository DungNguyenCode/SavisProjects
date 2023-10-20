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
    public class PaymenMethodService:IAllinterface<PaymentMethod>

    {
        private readonly ContextDb _dbContext;

        public PaymenMethodService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(PaymentMethod item)
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
            var acc = _dbContext.PaymentMethods.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            return await _dbContext.PaymentMethods.ToListAsync();
        }

        public async Task<bool> Update(PaymentMethod item)
        {
            var temp = _dbContext.PaymentMethods.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
              
                temp.Status = item.Status;
                temp.Method = item.Method;
                temp.Description = item.Description;
                temp.Id_order = item.Id_order;
                temp.Last_modified_date = DateTime.Now;
                temp.TotalMoney = item.TotalMoney;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
