using Microsoft.EntityFrameworkCore;
using System.Linq;
using Messaia.Net.Data;
using Messaia.Net.Identity;
using Messaia.Net.Repository.Impl;

namespace WebApp
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UserRepository"/> class.
        /// </summary>
        public UserRepository(IDbContext context) : base(context)
        {
        }

        public override IQueryable<User> GetSingleQueryable()
        {
            return base.GetSingleQueryable().Include(x => x.Roles).ThenInclude(x => x.Role);
        }
    }
}