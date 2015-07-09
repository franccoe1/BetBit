using Newtonsoft.Json.Linq;

namespace BtceApi
{
    public class CouponResult
    {
        public Funds Funds { get; private set; }
        public int CouponAmount { get; private set; }
        public string CouponCurrency { get; private set; }
        public int TransID { get; private set; }

        private CouponResult() { }
        public static CouponResult ReadFromJObject(JObject o)
        {
            return new CouponResult()
            {
                Funds = Funds.ReadFromJObject(o["funds"] as JObject),
                CouponAmount = o.Value<int>("couponAmount"),
                CouponCurrency = o.Value<string>("couponCurrency"),
                TransID = o.Value<int>("transID")
            };
        }
    }
}
