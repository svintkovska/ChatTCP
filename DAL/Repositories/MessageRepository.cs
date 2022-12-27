using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class MessageRepository : IRepository<MessageEntity>
    {
        private AppEFContext _context = new AppEFContext();

        public void Create(MessageEntity item)
        {
            if (item != null)
            {
                _context.Messages.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            var tempMessage = _context.Messages.Find(id);
            if (tempMessage != null)
            {
                _context.Messages.Remove(tempMessage);
                _context.SaveChanges();
            }
        }

        public MessageEntity Find(int? id) => _context.Messages.Find(id);

        public IEnumerable<MessageEntity> GetAll() => _context.Messages;

        public void Update(MessageEntity item)
        {
            var tempMessage = _context.Messages.Find(item.Id);
            tempMessage.Text = item.Text;
            tempMessage.Time = item.Time;
            tempMessage.UserId = item.UserId;
            _context.SaveChanges();
        }
    }
}
