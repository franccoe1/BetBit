using BetBit.Frontend.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetBit.Frontend.Controllers
{
    public class AccountController : Controller
    {
        public User GetUser()
        {
            User user = new User();

            HttpCookie myCookie = new HttpCookie("BetBit");
            if (myCookie == null)
            {

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                user.Username = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                user.Password = new string(
                    Enumerable.Repeat(chars, 12)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                user.UserId = Guid.NewGuid();

                myCookie["BetBit"] = user.UserId.ToString();
                //myCookie["Password"] = user.Password;
                myCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(myCookie);
            }
            else
            {
                user.UserId = new Guid(myCookie.Value);
            }

            return user;
        }

    }
}
