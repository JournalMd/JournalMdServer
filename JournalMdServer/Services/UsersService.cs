using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using JournalMdServer.Helpers;
using JournalMdServer.Interfaces;
using JournalMdServer.Models;
using JournalMdServer.DTOs.Users;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class UsersService
    {
        private readonly IRepository<User> _repository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UsersService(IRepository<User> repository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _repository = repository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        /// <summary>
        /// Check login credentials and return token.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public async Task<LoginOutput> Authenticate(LoginInput userInput)
        {
            if (string.IsNullOrEmpty(userInput.Username) || string.IsNullOrEmpty(userInput.Password))
                return null;

            var user = await _repository.Query.SingleOrDefaultAsync(x => x.Username == userInput.Username);

            if (user == null)
                return null;

            // Check password
            if (!VerifyPasswordHash(userInput.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            _repository.Update(user);
            await _repository.Context.SaveChangesAsync();

            return new LoginOutput() { Token = user.Token };
        }

        /// <summary>
        /// Get User DTO for id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserOutput> GetById(long id)
        {
            User user = await _repository.Query.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<UserOutput>(user);
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="registerInput"></param>
        /// <returns></returns>
        public async Task<RegisterOutput> Create(RegisterInput registerInput)
        {
            // validation
            if (string.IsNullOrWhiteSpace(registerInput.Password))
                throw new AppException("Password is required");

            if (await _repository.Query.AnyAsync(x => x.Username == registerInput.Username))
                throw new AppException("Username \"" + registerInput.Username + "\" is already taken");

            CreatePasswordHash(registerInput.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Username = registerInput.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _repository.Insert(user);
            _repository.Context.SaveChanges();

            return _mapper.Map<RegisterOutput>(user);
        }

        // Update

        // UpdatePassword

        // https://github.com/cornflourblue/aspnet-core-3-registration-login-api/blob/master/Services/UserService.cs
        internal static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new AppException("password");

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Value cannot be empty or whitespace only string.", "password");

            if (password.Length < 8)
                throw new AppException("Password is to short.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        // https://github.com/cornflourblue/aspnet-core-3-registration-login-api/blob/master/Services/UserService.cs
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
                throw new AppException("password");

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Value cannot be empty or whitespace only string.", "password");

            if (storedHash.Length != 64)
                throw new AppException("Invalid length of password hash (64 bytes expected).", "passwordHash");

            if (storedSalt.Length != 128)
                throw new AppException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
