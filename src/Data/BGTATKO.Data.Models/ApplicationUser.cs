﻿// ReSharper disable VirtualMemberCallInConstructor
namespace BGTATKO.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BGTATKO.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
            this.Followers = new HashSet<UserFollower>();
            this.Following = new HashSet<UserFollower>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string ImageUrl { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserFollower> Followers { get; set; }

        public virtual ICollection<UserFollower> Following { get; set; }
    }
}
