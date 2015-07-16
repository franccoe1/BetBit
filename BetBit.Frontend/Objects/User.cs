using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetBit.Frontend.Objects
{
    public class User
    {

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }
    }
}