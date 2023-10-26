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
    public class ImageService : IAllinterface<Image>
    {
        private readonly ContextDb _dbContext;

        public ImageService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Image item)
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
            var acc = _dbContext.Images.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            return await _dbContext.Images.ToListAsync();
        }

        public async Task<Image> GetById(Guid Id)
        {
            var temp = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Image item)
        {
            var temp = _dbContext.Images.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.Status = item.Status;
                temp.Name = item.Name;
                temp.Id_Product_details = item.Id_Product_details;
                temp.ImageFile = item.ImageFile;
                temp.Last_modified_date = DateTime.Now;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
