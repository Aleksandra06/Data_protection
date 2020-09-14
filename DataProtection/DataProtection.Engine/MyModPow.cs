using System;
using System.Collections.Generic;
using System.Text;

namespace DataProtection.Engine
{
    public class MyModPow
    {
        public long Pow(long a, long x, long p)
        {
            var y = new long();
            var s = new long();
            var bin = Convert.ToString(x, 2);

            y = 1;
            s = a % p;
            if (bin[^1] != '0') {
                y = s;
            }

            for (var i = bin.Length - 2; i >= 0; i--) {
                s = (s * s) % p;

                var t = bin[i];
                var t1 = '0';
                if (bin[i] == '0') {
                    continue;
                }

                y *= s;
            }

            return y % p;
        }
    }
}
