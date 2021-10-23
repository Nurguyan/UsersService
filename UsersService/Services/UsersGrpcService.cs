using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Data;
using UsersService.Models;

namespace UsersService
{
    public class UsersGrpcService : UsersGrpc.UsersGrpcBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersGrpcService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async override Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var result = await _usersRepository.DeleteUserAsync(request.Id);
            return new DeleteUserResponse { IsSuccess = result };
        }

        public async override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = await _usersRepository.GetUserByIdAsync(request.Id);
            return new GetUserResponse() { UserDetail = _mapper.Map<UserDetail>(user) };
        }

        public async override Task<GetUsersResponse> GetUsers(GetUsersRequest request, ServerCallContext context)
        {
            var users = await _usersRepository.GetAllUsersAsync();
            return new GetUsersResponse { Users = { users.Select(_mapper.Map<UserDetail>) } };
        }

        public async override Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var updatedUser = await _usersRepository.UpdateUserAsync(_mapper.Map<User>(request.UserDetail));
            return new UpdateUserResponse() { UserDetail = _mapper.Map<UserDetail>(updatedUser)};
        }
    }
}
