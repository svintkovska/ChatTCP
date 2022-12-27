using DAL.Data;
using DAL.Interfaces;
using SomeeMSSQLConsole.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<UserEntity>
    {
        private AppEFContext _context = new AppEFContext();

        public void Create(UserEntity item)
        {
            if (item != null)
            {
                _context.Users.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            var tempUser = _context.Users.Find(id);
            if (tempUser != null)
            {
                _context.Users.Remove(tempUser);
                _context.SaveChanges();
            }
        }

        public UserEntity Find(int? id) => _context.Users.Find(id);
        public UserEntity Find(string email, string password)
        {
            if (IsUserInDB(email, password))
                return _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            return null;
        }
        private bool IsUserInDB(string email, string password)
        {
            return _context.Users.Count(u => u.Email == email && u.Password == password) > 0;
        }

        public IEnumerable<UserEntity> GetAll() => _context.Users;

        public void Update(UserEntity item)
        {
            var tempUser = _context.Users.Find(item.Id);
            tempUser.Name = item.Name;
            tempUser.Email = item.Email;
            tempUser.Password = item.Password;
            _context.SaveChanges();
        }
    }
}
