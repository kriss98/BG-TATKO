namespace BGTATKO.Services.Data.Impl
{
    using System.Linq;
    using System.Threading.Tasks;
    using BGTATKO.Data.Common.Repositories;
    using BGTATKO.Data.Models;
    using Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<T> GetUserByIdAsync<T>(string id)
        {
            return await this.userRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string firstName, string lastName, string phoneNumber, string email, string imageUrl, string id)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.Email = email;
            user.ImageUrl = imageUrl;

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }
    }
}
