﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Model
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Text { get; set; }

    }
}
