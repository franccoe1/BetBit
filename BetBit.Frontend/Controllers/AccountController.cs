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
        public JsonResult GetPublicUser()
        {
            return Json(GetUser(Request), JsonRequestBehavior.AllowGet);
        }

        public User GetUser(HttpRequestBase request)
        {
            User user = new User();

            HttpCookie myCookie = request.Cookies["BetBit"];
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
                user.Balance = 0;

                myCookie = new HttpCookie("BetBit");
                myCookie.Value = user.UserId.ToString();
                myCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(myCookie);

                BetBitEntities betBitEntities = new BetBitEntities();

                betBitEntities.Users.Add(new Users()
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password,
                    Balance = user.Balance

                });

                betBitEntities.SaveChanges();
            }
            else
            {
                user.UserId = new Guid(myCookie.Value);
            }
            return user;
        }

        public JsonResult UserLogin(string username, string password, string logout)
        {
            BetBitEntities betBitEntities = new BetBitEntities();
            HttpCookie myCookie = Request.Cookies["BetBit"];
            User user = new User();

            var User = betBitEntities.Users.FirstOrDefault(i => i.Username.Equals(username) && i.Password.Equals(password));

            if (User != null)
            {
                myCookie = new HttpCookie("BetBit");
                myCookie.Value = User.UserId.ToString();
                myCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(myCookie);

                user.Username = User.Username;
                user.Password = User.Password;
                user.UserId = User.UserId.Value;
            }
            if (logout.Equals("true"))
            {
                Response.Cookies["BetBit"].Expires = DateTime.Now.AddDays(-1);
                myCookie = null;

                if (myCookie == null)
                    return Logout();
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Logout()
        {
            User user = new User();
            HttpCookie myCookie = null;

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
            user.Balance = 0;

            myCookie = new HttpCookie("BetBit");
            myCookie.Value = user.UserId.ToString();
            myCookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(myCookie);

            BetBitEntities betBitEntities = new BetBitEntities();

            betBitEntities.Users.Add(new Users()
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                Balance = user.Balance
            });

            betBitEntities.SaveChanges();

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePassword(string username, string currentPassword, string newPassword)
        {
            BetBitEntities betBitEntities = new BetBitEntities();
            HttpCookie myCookie = Request.Cookies["BetBit"];
            //User user = new User();

            //get user
            var user = betBitEntities.Users.FirstOrDefault(i => i.Username.Equals(username) && i.Password.Equals(currentPassword));

            //new password
            if (user != null)
            {
                user.Password = newPassword;
                betBitEntities.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}
