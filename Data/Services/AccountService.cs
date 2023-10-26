﻿using Data.ContextDbSavis;
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
    public class AccountService : IAllinterface<Accounts>
    {
        private readonly ContextDb _dbContext;

        public AccountService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Accounts item)
        {
            if (item!=null)
            {
                await _dbContext.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var acc = _dbContext.Accounts.FirstOrDefault(a => a.Id == Id);

            if (acc!=null)
            {
                _dbContext.Remove(acc); 
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Accounts>> GetAll()
        {
           return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Accounts> GetById(Guid Id)
        {
            var temp = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Accounts item)
        {
            var temp = _dbContext.Accounts.FirstOrDefault(a => a.Id == item.Id);

            if (temp != null)
            {
                temp.Status = item.Status;
                temp.Email = item.Email;
                temp.Id_User = item.Id_User;
                temp.Password = item.Password;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
