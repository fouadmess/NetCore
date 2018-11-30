using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaia.Net.Identity;
using Messaia.Net.Security;

namespace WebApp.Security
{
    public interface IUserSecurityService : ISecurityService<User>
    {
    }

    public class UserSecurityService : SecurityServiceBase<User>, IUserSecurityService
    {
        public UserSecurityService(IAuthorizationService AuthorizationService)
            : base(AuthorizationService)
        {

        }
    }
}
