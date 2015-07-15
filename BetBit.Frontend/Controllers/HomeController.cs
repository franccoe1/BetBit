using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetBit.Frontend.Models;
using BtceApi;
using log4net;
using log4net.Config;

namespace BetBit.Frontend.Controllers
{

    public class HomeController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        //
        // GET: /Home/

        private const string key = "OWOKHELK-DI5J50DL-OJIKVH5K-LH4XBL7O-I8LRKJW6";
        private const string secret = "e2a798864cca17b5159f316e4356566e9e1e23d7cca43a7826e46bf13d968d69";

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Test()
        {
            BtceApix btceApix = new BtceApix(key, secret);

            return Json(btceApix.GetInfo(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RedeemCoupon(string coupon)
        {
            BtceApix btceApix = new BtceApix(key, secret);

            CouponResult couponResult = btceApix.RedeemCoupon(coupon);

            BetBitEntities betBitEntities = new BetBitEntities();
            AccountController accountController = new AccountController();
            betBitEntities.Coupon.Add(new Coupon()
                {
                    UserId = accountController.GetUser().UserId,
                    CouponAmount = couponResult.CouponAmount,
                    CouponCode = coupon
                });
            betBitEntities.SaveChanges();
            return Json(couponResult, JsonRequestBehavior.AllowGet);
        }

        //levantamento de Eres
        public JsonResult CreateCoupon(int amount)
        {
            BtceApix btceApix = new BtceApix(key, secret);

            CouponCreate couponCreate = btceApix.CreateCoupon("EUR", amount);
            try
            {
                if (couponCreate != null)
                {
                    BetBitEntities betBitEntities = new BetBitEntities();
                    AccountController accountController = new AccountController();
                    betBitEntities.Coupon.Add(new Coupon()
                    {
                        UserId = accountController.GetUser().UserId,
                        CouponAmount = amount,
                        CouponCode = couponCreate.Coupon,
                        TransId = couponCreate.TransID
                    });
                    betBitEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Debug("ola", ex);
            }
            return Json(couponCreate, JsonRequestBehavior.AllowGet);
        }

    }
}
