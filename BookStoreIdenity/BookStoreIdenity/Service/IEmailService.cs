using BookStoreIdenity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreIdenity.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);

       // Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);

       // Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}
