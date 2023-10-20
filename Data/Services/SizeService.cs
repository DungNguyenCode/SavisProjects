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
    public class SizeService : IAllinterface<Size>
    {
        private readonly ContextDb _dbContext;

        public SizeService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Size item)
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
            var acc = _dbContext.Sizes.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Size>> GetAll()
        {
            return await _dbContext.Sizes.ToListAsync();
        }

        public async Task<bool> Update(Size item)
        {
            var temp = _dbContext.Sizes.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.Last_modified_date = DateTime.Now;
                temp.Status = item.Status;
                temp.Name = temp.Name;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
