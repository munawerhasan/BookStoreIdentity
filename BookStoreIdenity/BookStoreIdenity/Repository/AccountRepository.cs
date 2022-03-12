using BookStoreIdenity.Models;
using BookStoreIdenity.Service;
using BookStoreIdenity.Shared;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        //private readonly IEmailService _emailService;
        //private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            //RoleManager<IdentityRole> roleManager,
            IUserService userService
            //IEmailService emailService,
            //IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_roleManager = roleManager;
            _userService = userService;
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
                UserName = userModel.Email

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

        public async Task<Claims> PasswordSignInAsync(SignInModel signInModel)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(signInModel.Email).ConfigureAwait(false);
            if (user !=null)
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
                if (result.Succeeded)
                {
                    Claims response = new Claims()
                    {
                        Name = user.FirstName,
                        UserName = user.UserName,
                        Email = user.Email,
                       // AuthToken = _tokens.GenerateEncodedToken(user.UserName),
                    };

                    return response;
                }

                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

           
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

    }
}
