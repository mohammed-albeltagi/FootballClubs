using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace FootballClubs.Models
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
    {
        private readonly DataContext _db;

        public UserStore(DataContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(SetUserNameAsync));
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)null);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _db.Add(user);

            await _db.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                return Task.FromResult(IdentityResult.Failed());
            }
            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Task.FromResult(IdentityResult.Failed());
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _db.Remove(user);

            int i = await _db.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }
        
        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                return await _db.Users.FindAsync(id);
            }
            else
            {
                return await Task.FromResult((User)null);
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _db.Users
                           .AsAsyncEnumerable()
                           .SingleOrDefault(p => p.UserName.Equals(normalizedUserName, StringComparison.OrdinalIgnoreCase), cancellationToken);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult((object)null);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            Role role = null;

            try
            {
                role = _db.Set<Role>().Where(mr => mr.Name == roleName).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            _db.Set<UserRole>().Add(new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            return _db.SaveChangesAsync();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            bool isInRole = false;

            Role roleDb = _db.Roles.FirstOrDefault(r => string.Compare(r.Name, roleName) == 0);
            User userDb = _db.Users.Find(user.Id);

            if (roleDb != null && userDb != null)
            {
                UserRole userRole = userDb.UserRoles?.FirstOrDefault(ur => ur.RoleId == roleDb.Id);
                isInRole = userRole != null;
            }


            return Task.FromResult<bool>(isInRole);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
