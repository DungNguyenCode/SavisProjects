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
    public class VorcherDetalService : IAllinterface<VorcherDetail>
    {
        private readonly ContextDb _dbContext;

        public VorcherDetalService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(VorcherDetail item)
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
            var acc = _dbContext.VorcherDetails.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<VorcherDetail>> GetAll()
        {
            return await _dbContext.VorcherDetails.ToListAsync();
        }

        public async Task<VorcherDetail> GetById(Guid Id)
        {
            var temp = await _dbContext.VorcherDetails.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(VorcherDetail item)
        {
            var temp = _dbContext.VorcherDetails.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.AfterPrice = item.AfterPrice;
                temp.BeforPrice = item.BeforPrice;
                temp.DiscountPrice = item.DiscountPrice;
                temp.Id_order = item.Id_order;
                temp.Id_Voucher = item.Id_Voucher;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
