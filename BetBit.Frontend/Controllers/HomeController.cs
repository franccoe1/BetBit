using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetBit.Frontend.Models;
using BtceApi;

namespace BetBit.Frontend.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Test()
        {
            BtceApix btceApix = new BtceApix("OM0BU74S-GB21S7OL-FXLR96L8-RMB5BJ27-O34RJAJ3", "c1f3662a001cbbf88b970d30f88e3849c16a221b1711f0b7d0c5601d432531d9");

            return Json(btceApix.GetInfo(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RedeemCoupon(string userId, string coupon)
        {
            BtceApix btceApix = new BtceApix("OM0BU74S-GB21S7OL-FXLR96L8-RMB5BJ27-O34RJAJ3", "c1f3662a001cbbf88b970d30f88e3849c16a221b1711f0b7d0c5601d432531d9");
            
            CouponResult couponResult = btceApix.RedeemCoupon(coupon);
            
            BetBitEntities betBitEntities = new BetBitEntities();
            betBitEntities.Coupon.Add(new Coupon()
                {
                    UserId = new Guid(userId),
                    CouponAmount = couponResult.CouponAmount,
                    CouponCode = coupon
                });
            return Json(couponResult, JsonRequestBehavior.AllowGet);
        }

    }
}
