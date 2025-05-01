using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            if (_users.Any(u => u.Email == user.Email))
            {
                throw new InvalidOperationException("A user with the same email already exists.");
            }

            user.Id = Guid.NewGuid();
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}