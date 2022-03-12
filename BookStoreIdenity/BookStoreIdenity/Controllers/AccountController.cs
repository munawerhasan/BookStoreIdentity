using BookStoreIdenity.Models;
using BookStoreIdenity.Repository;
using BookStoreIdenity.Service;
using BookStoreIdenity.Shared;
using Common.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreIdenity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        public AccountController(IAccountRepository accountRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
        }
        [Route("signup")]
        [HttpPost]
        public async Task<ApiResponse> Signup([FromBody] SignUpUserModel userModel)
        {
           
            ApiResponse response = new ApiResponse();
            try
            {
                bool userNameExistsAlready = await _accountRepository.IsValidUser(userModel.FirstName).ConfigureAwait(false);

                if (!userNameExistsAlready)
                {
                    response.Message = "Account with this username already exist";
                    return response;
                   
                }
                bool userEmailExistsAlready = await _accountRepository.IsValidEmail(userModel.Email).ConfigureAwait(false);
                if (!userEmailExistsAlready)
                {
                    response.Message = "Account with this email already exist";
                    return response;
                 
                }
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (result.Succeeded)
                {
                    response.Message = "Account Created successfully";
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse> Login([FromBody] SignInModel signInModel)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                
                Claims claims = await _accountRepository.PasswordSignInAsync(signInModel).ConfigureAwait(false);
                if (claims != null)
                {
                    UserEmailOptions options = new UserEmailOptions
                    {
                        ToEmails = new List<string>() { "munawerhasan52@gmail.com" },
                        PlaceHolders = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("{{UserName}}", "John")
                        }
                    };

                    await _emailService.SendTestEmail(options);
                    response.ResponseData = claims;
                    response.Message = "Login successfully";
                    response.Status = 1;

                }
                else
                {
                    response.ResponseData = claims;
                    response.Message = "Username & Password is invalid!";
                    response.Status = 0;
                }

            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<ApiResponse> ChangePassword(ChangePasswordModel model)
        {
            ApiResponse response = new ApiResponse();
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                   
                    response.Message = "Password changed successfully";
                    response.Status = 1;

                }
                else
                {
                    response.Message = "Password is not change!";
                    response.Status = 0;
                }
            }
            return response;
        }
    }
}

