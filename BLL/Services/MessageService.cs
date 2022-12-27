using BLL.Interfaces;
using BLL.Models;
using DAL.Data.Entities;
using DAL.Repositories;
using SomeeMSSQLConsole.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class MessageService : IService<MessageDTO>
    {
        private readonly MessageRepository _messageRepository = new MessageRepository();

        public void Create(MessageDTO item)
        {
            if (item != null)
            {
                _messageRepository.Create(TranslateMessageDTOToMessageEntity(item));
            }
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                _messageRepository.Delete(id);
            }
        }

        public MessageDTO Find(int? id)
        {
            if (id != null)
                return TranslateMessageEnitityToMessageDTO(_messageRepository.Find(id));
            return null;
        }

        public IList<MessageDTO> GetAll()
        {
            var list = new List<MessageDTO>();
            foreach (var msg in _messageRepository.GetAll())
            {
                list.Add(TranslateMessageEnitityToMessageDTO(msg));
            }
            return list;
        }

        public void Update(MessageDTO item)
        {
            if (item != null)
            {
                _messageRepository.Update(TranslateMessageDTOToMessageEntity(item));
            }

        }

        private MessageDTO TranslateMessageEnitityToMessageDTO(MessageEntity messageEntity)
        {
            if (messageEntity != null)
                return new MessageDTO()
                {
                    Id = messageEntity.Id,
                    Time = messageEntity.Time,
                    Text = messageEntity.Text,
                    UserId = messageEntity.UserId,
                    User = new UserDTO()
                    {
                        Id = messageEntity.User.Id,
                        Name = messageEntity.User.Name,
                        Email = messageEntity.User.Email,
                        Password = messageEntity.User.Password
                    }
                };
            return null;
        }

        private MessageEntity TranslateMessageDTOToMessageEntity(MessageDTO messageDTO)
        {
            if (messageDTO != null)
                return new MessageEntity()
                {
                    Id = messageDTO.Id,
                    Time = messageDTO.Time,
                    Text = messageDTO.Text,
                    UserId = messageDTO.UserId,
                    User = new UserEntity()
                    {
                        Id = messageDTO.User.Id,
                        Name = messageDTO.User.Name,
                        Email = messageDTO.User.Email,
                        Password = messageDTO.User.Password
                    }
                };

            return null;
        }
    }
}
