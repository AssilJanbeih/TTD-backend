using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTD_Backend;
using TTD_Backend.DTOs;

namespace TTTD_Context.Services
{
    public class UserService
    {
        private readonly TTTDContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(TTTDContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<User>> GetUsersByJobTypeAsync(int jobTypeId)
        {
            return await _context.Users
                .Where(u => u.JobTypeId == jobTypeId)
                .ToListAsync();
        }

        public async Task<List<GetUsersDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.JobType) 
                .Select(u => new GetUsersDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Title = u.Title,
                    Active = u.Active,
                    Role = u.JobType.Name 
                })
                .ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(string id, User updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            user.Name = updatedUser.Name;
            user.Title = updatedUser.Title;
            user.Active = updatedUser.Active;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _userManager.DeleteAsync(user);
        }

        public async Task<int> GetJobTypeIdByNameAsync()
        {
            var jobType = await _context.JobTypes
                .Where(j => j.Id == 3)
                .FirstOrDefaultAsync();

            return jobType?.Id ?? 0;
        }
    }
}
