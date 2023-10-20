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
    public class NotifService:IAllinterface<Notifi>
    {
        private readonly ContextDb _dbContext;

        public NotifService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Notifi item)
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
            var acc = _dbContext.Notifis.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Notifi>> GetAll()
        {
            return await _dbContext.Notifis.ToListAsync();
        }

        public async Task<bool> Update(Notifi item)
        {
            var temp = _dbContext.Notifis.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.Status = item.Status;
                temp.Last_modified_date = DateTime.Now;
                temp.Id_account = item.Id_account;
                temp.Noti_conten = item.Noti_conten;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
