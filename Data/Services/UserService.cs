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
    public class UserService:IAllinterface<User>
    {
        private readonly ContextDb _dbContext;

        public UserService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(User item)
        {
            if (item != null)
            {
                item.Status = 1;
                item.Avatar = "none";
                item.Id_Role = Guid.Parse("f52854d7-cac0-44c7-bb87-7cd575648091");
                await _dbContext.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var acc = _dbContext.Users.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(Guid Id)
        {
            var temp = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(User item)
        {
            var temp = _dbContext.Users.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.Status = item.Status;
                temp.PhoneNumber = item.PhoneNumber;
                temp.Fullname = item.Fullname;
                temp.Dateofbirth = item.Dateofbirth;
                temp.Email = item.Email;
                temp.Gender = item.Gender;
                temp.Password =item.Password;
                temp.Last_modified_date = DateTime.Now;
                temp.Avatar = item.Avatar;
                temp.Id_Role = item.Id_Role;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
