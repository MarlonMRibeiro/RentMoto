﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        [EmailAddress()]
        public string Email { get; set; }
        public string Name { get; set; }
        public bool Admin { get; set; }
    }
}
