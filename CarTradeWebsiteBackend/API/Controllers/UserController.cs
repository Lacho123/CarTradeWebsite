﻿using CarTradeWebsite.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTradeWebsite.API.Controllers
{
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository { get; set; }

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet("users/count")]
        public async Task<ActionResult<int>> GetUserCount()
        {
            int count = await this._userRepository.GetTotalUsersCountAsync();

            return Ok(count);
        }

        [HttpGet("users/get")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            IEnumerable<UserModel> users = await this._userRepository.GetUsersAsync();

            if(users == null || users.Count() == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }

        [HttpGet("users/get/{ID}")]
        public async Task<ActionResult<UserModel>> GetUserById(Guid ID)
        {
            UserModel user = await this._userRepository.GetUserByIDAsync(ID);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("users/new")]
        public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel user)
        {
            UserModel createdUser = await this._userRepository.CreateUserAsync(user);

            if(createdUser == null)
            {
                return BadRequest();
            }

            return Ok(createdUser);
        }

        [HttpPut("users/{userId}/edit")]
        public async Task<ActionResult<UserModel>> EditUser(Guid userId, UserModel user)
        {
            UserModel updatedUser = await this._userRepository.EditUserAsync(userId, user);
        
            if(updatedUser == null)
            {
                return BadRequest();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("users/{userId}/delete")]
        public async Task<ActionResult<bool>> DeleteUser(Guid userId)
        {
            bool isDeleted = await this._userRepository.DeleteUserAsync(userId);

            if (!isDeleted)
            {
                return NotFound(userId);
            }

            return Ok();
        }
    }
}
