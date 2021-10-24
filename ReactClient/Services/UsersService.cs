using ReactClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactClient.Services
{
    public interface IUsersService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
    public class UsersService : IUsersService
    {
        private readonly IUsersGrpcClient _client;

        public UsersService(IUsersGrpcClient client)
        {
            _client = client;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _client.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _client.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _client.GetUserByIdAsync(id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _client.UpdateUserAsync(user);
        }
    }
}
