using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SQLUsersRepository : IUsersRepository
    {
        private readonly FitnessPlannerContext _context;
        public SQLUsersRepository(FitnessPlannerContext context)
        {
            context = _context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

            throw new NotImplementedException();
        }

        public async Task<User> GetLoggingUsersAsync(string userName, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName && x.Password == password);

            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsernameAsync(string userName)
        {
            await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.UpdateRange(user);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
