using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Repositories;
using SomeeMSSQLConsole.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserService : IService<UserDTO>
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public void Create(UserDTO item)
        {
            if (item != null)
            {
                _userRepository.Create(TranslateUserDTOToUserEntity(item));
            }
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                _userRepository.Delete(id);
            }
        }

        public UserDTO Find(int? id)
        {
            if (id != null)
                return TranslateUserEntityToUserDTO(_userRepository.Find(id));
            return null;
        }
        public UserDTO SearchUser(string email, string password)
        {
            return TranslateUserEntityToUserDTO(_userRepository.Find(email, password));
        }
        public IList<UserDTO> GetAll()
        {
            var list = new List<UserDTO>();
            foreach (var user in _userRepository.GetAll())
            {
                list.Add(TranslateUserEntityToUserDTO(user));
            }
            return list;
        }

        public void Update(UserDTO item)
        {
            if (item != null)
            {
                _userRepository.Update(TranslateUserDTOToUserEntity(item));
            }
        }

        private UserEntity TranslateUserDTOToUserEntity(UserDTO userDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserDTO, UserEntity>()).CreateMapper();
            if (userDTO != null)
                return new UserEntity()
                {
                    Id = userDTO.Id,
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password                  
                };
            return null;
        }

        private UserDTO TranslateUserEntityToUserDTO(UserEntity userEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserEntity, UserDTO>()).CreateMapper();

            if (userEntity != null)
                return new UserDTO()
                {
                    Id = userEntity.Id,
                    Name = userEntity.Name,
                    Email = userEntity.Email,
                    Password = userEntity.Password
                };
            return null;
        }
    }
}
