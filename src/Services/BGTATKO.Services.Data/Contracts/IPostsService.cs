﻿namespace BGTATKO.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task CreateAsync(string title, string content, int categoryId, string userId);

        IEnumerable<T> GetAllByCategoryId<T>(int categoryId, int take, int skip);

        int GetPostsCountByCategoryId(int categoryId);
    }
}