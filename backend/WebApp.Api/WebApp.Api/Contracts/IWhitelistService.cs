using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Api.Models;

namespace WebApp.Api.Contracts
{
    public interface IWhitelistService
    {
        public Task<bool> AddToWhitelist(WhitelistModel whitelist);
    }
}
