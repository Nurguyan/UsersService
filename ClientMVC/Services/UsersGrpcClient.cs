using AutoMapper;
using ClientMVC.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Services
{
    public interface IUsersGrpcClient
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }

    internal sealed class UsersGrpcClient : IUsersGrpcClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsersGrpcClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            Console.WriteLine($"--> Requesting to delete user with id={id}...");
            var channel = GrpcChannel.ForAddress(_configuration["UsersGrpcService"]);
            var client = new UsersGrpc.UsersGrpcClient(channel);

            try
            {
                var response = await client.DeleteUserAsync(new DeleteUserRequest { Id = id });
                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not call GRPC Server {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            Console.WriteLine($"--> Requesting all users...");
            var channel = GrpcChannel.ForAddress(_configuration["UsersGrpcService"]);
            var client = new UsersGrpc.UsersGrpcClient(channel);

            try
            {
                var response = await client.GetUsersAsync(new GetUsersRequest());
                return _mapper.Map<IEnumerable<User>>(response.Users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not call GRPC Server {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            Console.WriteLine($"--> Requesting user with id={id}...");
            var channel = GrpcChannel.ForAddress(_configuration["UsersGrpcService"]);
            var client = new UsersGrpc.UsersGrpcClient(channel);

            try
            {
                var response = await client.GetUserAsync(new GetUserRequest { Id = id});
                return _mapper.Map<User>(response.UserDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not call GRPC Server {ex.Message}");
                return null;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            Console.WriteLine($"--> Requesting to update user with id={user.Id}...");
            var channel = GrpcChannel.ForAddress(_configuration["UsersGrpcService"]);
            var client = new UsersGrpc.UsersGrpcClient(channel);

            try
            {
                var response = await client.UpdateUserAsync(new UpdateUserRequest{ UserDetail = _mapper.Map<UserDetail>(user) });
                return _mapper.Map<User>(response.UserDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}
