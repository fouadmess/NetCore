using Messaia.Net.Api;
using Messaia.Net.Identity;
using Messaia.Net.Security;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Secure(typeof(ISecurityService<User>))]
    public class UserController : CRUDController<IUserService, User>
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes an instance of the <see cref="UserController"/> class.
        /// </summary>
        public UserController(IUserService userService) : base(userService)
        {
            this.userService = userService;
            var t = this.userService.GetAsync(x => x.Id == 1).GetAwaiter().GetResult();
        }
    }
}