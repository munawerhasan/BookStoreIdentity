using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreIdenity.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}
