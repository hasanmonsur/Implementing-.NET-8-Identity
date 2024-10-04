using WebIdentityApp.Models;

namespace WebIdentityApp.Contacts
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<bool> ConfirmEmailAsync(string userId);
    }
}
