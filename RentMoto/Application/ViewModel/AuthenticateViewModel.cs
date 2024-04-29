using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class AuthenticateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

        public AuthenticateViewModel(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Login = user.UserName;
            Token = token;
        }
    }
}
