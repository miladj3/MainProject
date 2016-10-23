using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCUI.Helpers
{
    public static class CalculateDiscountOfPrice
    {
        public static Int64 CalculateDiscountPrice(this Int32 price, Int32 discount)=> 
            (Int64)((price - (price * discount) / 100));
    }
}