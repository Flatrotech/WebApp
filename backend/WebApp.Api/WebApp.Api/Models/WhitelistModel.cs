using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Api.Models
{
    public class WhitelistModel
    {
        public string NewUser { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
        
        public string FtpUser { get; set; }

        public string Password { get; set; }
    }
}
