using Application.Dtos.UserDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IValidator<User> _valiadator;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IValidator<User> valiadator, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _valiadator = valiadator;
            _mapper = mapper;
        }   

        public async Task<UserDto> AddUserAsync(RegisterDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                ValidationResult result = await _valiadator.ValidateAsync(user);

                if (!result.IsValid) { return null; }

                await _usersRepository.AddUserAsync(user); 
                return _mapper.Map<UserDto>(user);

            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }

        public async Task<UserDto> ChangeEmailAsync(string username, string email)
        {
            try
            {
                var user = await _usersRepository.GetUserByUsernameAsync(username);
                User newUserInfo = new User
                {
                    UserName = username,
                    EmailAddress = email,
                };

                var newUser = _mapper.Map(newUserInfo,user);
                ValidationResult result = await  _valiadator.ValidateAsync(newUser);
                if (!result.IsValid) { return null; }


                await _usersRepository.UpdateUserAsync(newUser);
                return _mapper.Map<UserDto>(newUser);


            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }

        public async Task<UserDto> ChangePasswordAsync(string username, string password)
        {
            try
            {
                var user = await _usersRepository.GetUserByUsernameAsync(username);
                User newUserInfo = new User
                {
                    UserName = username,
                    Password = password
                };
                var newUser = _mapper.Map(newUserInfo, user);

                ValidationResult result = await   _valiadator.ValidateAsync(newUser);
                if ( !result.IsValid) { return null; }

                await _usersRepository.UpdateUserAsync(newUser);
                return _mapper.Map<UserDto>(newUser);


            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }

        public async Task DeleteUserAsync(string username)
        {
            try
            {
                var user = await _usersRepository.GetUserByUsernameAsync(username);
                await _usersRepository.DeleteUserAsync(user);
            }
            catch (Exception)
            {
                throw new NotImplementedException();

            }
        }

        public async Task<UserDto> GetUserAsync(LoginDto userCredentials)
        {
            try
            {
                var user = await  _usersRepository.GetLoggingUsersAsync(userCredentials.Username, userCredentials.Password);
                return _mapper.Map<UserDto>(user);

            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _usersRepository.GetUserByUsernameAsync(username);
                return _mapper.Map<UserDto>(user);
             
            }
            catch (Exception)
            {
                throw new NotImplementedException();

            }
        }
    }
}
