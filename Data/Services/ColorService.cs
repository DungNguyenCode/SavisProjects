using Data.ContextDbSavis;
using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

using Color = Data.Models.Color;

namespace Data.Services
{
    public class ColorService : IAllinterface<Color>
    {
        private readonly ContextDb _dbContext;

        public ColorService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Color item)
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
            var acc = _dbContext.Colors.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Color>> GetAll()
        {
            return await _dbContext.Colors.ToListAsync();
        }

        public async Task<Color> GetById(Guid Id)
        {
            var temp = await _dbContext.Colors.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Color item)
        {
            var temp = _dbContext.Colors.FirstOrDefault(a => a.Id == item.Id);

            if (temp != null)
            {
                temp.Status = item.Status;
                temp.Name = item.Name;
                item.Last_modified_date = DateTime.Now;
               
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
