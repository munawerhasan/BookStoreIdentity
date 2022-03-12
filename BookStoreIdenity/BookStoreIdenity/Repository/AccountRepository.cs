using BookStoreIdenity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreIdenity.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IUserService _userService;
        //private readonly IEmailService _emailService;
        //private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager
            //SignInManager<ApplicationUser> signInManager,
            //RoleManager<IdentityRole> roleManager,
            //IUserService userService,
            //IEmailService emailService,
            //IConfiguration configuration
            )
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            //_roleManager = roleManager;
            //_userService = userService;
            //_emailService = emailService;
            //_configuration = configuration;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {

            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.FirstName

            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public async Task<bool> IsValidUser(string userName)
        {
            bool isValid = false;
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
                if (user == null)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
            }
            return isValid;
        }
        public async Task<bool> IsValidEmail(string userEmail)
        {
            bool isValid = false;
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(userEmail).ConfigureAwait(false);
                if (user == null)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
            }
            return isValid;
        }

    }
}
