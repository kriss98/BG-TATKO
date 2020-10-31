namespace BGTATKO.Services.Data.Impl
{
    using System.Collections.Generic;
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

        private readonly IDeletableEntityRepository<UserFollower> userFollowerRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<UserFollower> userFollowerRepository)
        {
            this.userRepository = userRepository;
            this.userFollowerRepository = userFollowerRepository;
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

        public async Task FollowUser(string followerId, string id)
        {
            var userFollower = new UserFollower
            {
                UserId = id,
                FollowerId = followerId,
            };

            await this.userFollowerRepository.AddAsync(userFollower);
            await this.userFollowerRepository.SaveChangesAsync();
        }

        public async Task UnfollowUser(string followerId, string id)
        {
            var userFollower = this.userFollowerRepository.All()
                .FirstOrDefault(x => x.FollowerId == followerId && x.UserId == id);

            this.userFollowerRepository.Delete(userFollower);
            await this.userFollowerRepository.SaveChangesAsync();
        }

        public bool CheckIfUserFollowsUser(string currentUserId, string userId)
        {
            return this.userFollowerRepository.All()
                .FirstOrDefault(x => x.FollowerId == userId && x.UserId == currentUserId) != null;
        }

        public IEnumerable<T> GetAllUsers<T>()
        {
            return this.userRepository.All().To<T>().ToList();
        }
    }
}
