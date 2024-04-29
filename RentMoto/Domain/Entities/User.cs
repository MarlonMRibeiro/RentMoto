using Domain.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeleteAt { get; set; }

        public static User Create()
        {
            var user = new User();

            return user;
        }

        public User SetUserName(string userName)
        {
            UserName = userName;

            return this;
        }

        public User SetName(string name)
        {
            Name = name;

            return this;
        }

        public User SetEmail(string email)
        {
            Email = email;

            return this;
        }


    }
}
