using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.Models;

namespace web_api_eventz.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        
    }
}