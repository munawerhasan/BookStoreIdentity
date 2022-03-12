using BookStoreIdenity.Models;
using BookStoreIdenity.Repository;
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

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
    }
}
