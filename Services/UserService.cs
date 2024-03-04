using GlassECommerce.Models;
using GlassECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlassECommerce.Services
{
    public class UserService: ControllerBase, IUserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager) {
            _userManager = userManager;
        }
    }
}
