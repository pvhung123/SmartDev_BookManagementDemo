using BookService.Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookService.Application.Domain.Repository;

namespace BookService.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {            
            try
            {
                var books = await _userRepository.GetAllAsync();

                return books;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetAsync(int id)
        {
            try
            {
                var User = await _userRepository.GetAsync(id);

                return User;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
