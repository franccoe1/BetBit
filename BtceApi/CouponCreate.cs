using Newtonsoft.Json.Linq;

namespace BtceApi
{
    public class CouponCreate
    {
        public Funds Funds { get; private set; }
        public int Coupon { get; private set; }
        public int TransID { get; private set; }

        private CouponCreate() { }
        public static CouponCreate ReadFromJObject(JObject o)
        {
            return new CouponCreate()
            {
                Funds = Funds.ReadFromJObject(o["funds"] as JObject),
                Coupon = o.Value<int>("coupon"),
                TransID = o.Value<int>("transID")
            };
        }
    }
}
