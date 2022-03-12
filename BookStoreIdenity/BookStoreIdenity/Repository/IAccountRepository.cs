using BookStoreIdenity.Models;
using BookStoreIdenity.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreIdenity.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<bool> IsValidUser(string userName);
        Task<bool> IsValidEmail(string userEmail);
        Task<Claims> PasswordSignInAsync(SignInModel signInModel);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
    }
}
