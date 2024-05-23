using Blog.Common.Dtos;
using Blog.Common.Models.User;
using Blog.Data.Repositories;
using Blog.Services.Api.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Blog.Services.Api.User
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var user = await _userRepository.GetAll();
            return user.ParsToModels();
        }


        public async Task<UserDto> GetUserByid(Guid userid)
        {
            var user = await _userRepository.GetById(userid);
            return user.ParsToModel();

        }

       public  async Task<UserDto> AddUser(CreateUserModel model)
        {
            await IsExist(model.UserName);

            var user = new Data.Entities.User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
            };

            var password = new PasswordHasher<Data.Entities.User>().HashPassword(user, model.Password);
            user.PasswordHash = password;
            await _userRepository.Add(user);
            return user.ParsToModel();
        }

        public async Task<UserDto> Login(Login model)
        {
            var user = await _userRepository.GetByUsername(model.Username);
            if (user is null) throw new Exception("Invalid usernmae");

            var result = new PasswordHasher<Data.Entities.User>().VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid Password");
            return user.ParsToModel();

        }

        public async Task<UserDto> UpdateUser(Guid userid,UpdateUserModel model)
        {
            var user = await _userRepository.GetById(userid);
            var check = false;

            if(!string.IsNullOrWhiteSpace(model.FirstName))
            {
                user.FirstName = model.FirstName;
                check = true;
            }
            if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                user.LastName = model.LastName;
                check = true;
            }
            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                await IsExist(model.UserName);
                user.UserName = model.UserName;
                check = true;
            }
            if(check)
                await _userRepository.Update(user);
            return user.ParsToModel();
        }

        public async Task<string> DeleteUser(Guid userid)
        {
            var user = await _userRepository.GetById(userid);
            await _userRepository.Delete(user);
            return "User Successful Delete";
        }

        private async Task IsExist(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user is not null) throw new Exception("you cannot pick this username uo cos it is alredy taken");
        }
    }
}
