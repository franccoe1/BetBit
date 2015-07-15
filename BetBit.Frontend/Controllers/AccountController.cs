using BetBit.Frontend.Models;
using BetBit.Frontend.Objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            HttpCookie myCookie = Request.Cookies["BetBit"];
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

                myCookie = new HttpCookie("BetBit");
                myCookie.Value = user.UserId.ToString();
                myCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(myCookie);

                BetBitEntities betBitEntities = new BetBitEntities();

                betBitEntities.Users.Add(new Users()
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password
                });

                betBitEntities.GetValidationErrors();
                betBitEntities.SaveChanges();


            }
            else
            {
                user.UserId = new Guid(myCookie.Value);
            }
            return user;
        }

    }
}
