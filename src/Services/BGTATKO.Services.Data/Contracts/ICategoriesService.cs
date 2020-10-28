﻿namespace BGTATKO.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string description, string imageUrl);
    }
}
