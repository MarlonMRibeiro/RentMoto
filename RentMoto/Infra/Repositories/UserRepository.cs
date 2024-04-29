using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected RentMotoContext _context;

        public UserRepository(RentMotoContext context)
        {
            _context = context;
        }

    }

}
