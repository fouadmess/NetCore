using Messaia.Net.Identity;
using Messaia.Net.Repository;
using Messaia.Net.Service;
using Messaia.Net.Service.Impl;

namespace WebApp.Services
{
    public interface IUserService : IEntityService<User> { }

    public class UserService : EntityService<User>, IUserService
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
            : base(userRepository, unitOfWork) { }
    }
}
