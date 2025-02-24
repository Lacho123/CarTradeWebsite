﻿using CarTradeWebsite.Context;
using CarTradeWebsite.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarTradeWebsite.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static DatabaseContext context = new DatabaseContext();

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            if(context.Users.Any(x => x.Email == user.Email))
            {
                return null;
            }

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            return await context.Users
                .CountAsync();
        }

        public async Task<UserModel> GetUserByIDAsync(Guid ID)
        {
            return await context.Users
                .Where(user => user.ID == ID)
                .FirstAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await context.Users
                .ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid ID)
        {
            UserModel userToRemove = await context.Users
                .Where(user => user.ID == ID)
                .FirstAsync();

            if(userToRemove != null)
            {
                context.Users.Remove(userToRemove);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<UserModel> EditUserAsync(Guid userId, UserModel user)
        {
            UserModel userToUpdate = await context.Users
                .Where(user => user.ID == userId)
                .FirstAsync();

            if(userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Email = user.Email;

                await context.SaveChangesAsync();
                return userToUpdate;
            }

            return null;
        }
    }
}
