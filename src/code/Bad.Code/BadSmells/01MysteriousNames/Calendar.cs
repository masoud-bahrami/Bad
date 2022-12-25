﻿using System.Collections.Generic;
using System;

namespace Bad.Code.BadSmells._01MysteriousNames
{
    public class Calendar
    {
        public void Subscribe(User user)
        {

        }
    }

    public class Role
    {
    }

    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Role> Roles { get; set; }
        public string UserName { get; set; }
        private string HashedPassword { get; set; }
    }
}